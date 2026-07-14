# CynoApp

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=.net)
![EF Core](https://img.shields.io/badge/EF_Core-10.0-5C2D91?style=flat&logo=nuget)
![SQLite](https://img.shields.io/badge/SQLite-database-07405e?logo=sqlite&logoColor=white)
![License](https://img.shields.io/badge/license-MIT-green)

A REST API for managing breeders, litters, and their free-publication limits (benefits), built with a Clean N-Tier architecture on top of EF Core.

## Tech Stack

- .NET, ASP.NET Core Web API
- Entity Framework Core — code-first, `AppDbContext`
- SQLite — local development database
- Repository / Service pattern — data access via `AppDbContext`, business rules in the service layer
- Dependency Injection — built-in .NET DI container

## Architecture

The project follows a Clean N-Tier architecture, split into three main layers:

- **CA.DAL** (Data Access Layer) — entities (`Entities`), enums (`Enums`), EF Core context, seed data (`Persistence.Seed`).
- **CA.BLL** (Business Logic Layer) — services (`Services`), interfaces (`Interfaces`), DTOs (`DTOs`).
- **CA.API** (Presentation Layer) — controllers, DI composition root.

Domain entities protect their state via private setters; business rules (e.g. checking the publication limit) are encapsulated in entity methods (`BreederBenefit.EnlargeUsedCount`) and in the service layer.

### Key Entities

| Entity | Table | Description |
|---|---|---|
| `Litter` | `Litters` | A litter tied to a breeder (`BreederId`) with a status (`LitterStatus`: `Draft`, `Approved`, …). |
| `BreederBenefit` | `Benefits` | A breeder's free-publication limit (`FreeLimit`, `UsedCount`). |

## Getting Started

### Prerequisites

- .NET SDK 
- A REST client (Postman)
- `sqlite3` CLI or DB Browser for SQLite (optional, for inspecting the database)

### 1. Clone the repository

```bash
git clone <repo-url>
cd <project-folder>
```

### 2. Configure the connection string

The API uses SQLite for local development. Check `appsettings.json` / `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=ca.db"
}
```

### 3. Run the API

```bash
dotnet run --project CA.API
```

On first launch, the app applies EF Core migrations and seeds the database with test data via `DbSeeder`, including ready-made breeder scenarios for limit testing (see below).

## Inspecting the Database

```bash
sqlite3 ca.db
```

```sql
.tables
.schema Benefits

SELECT BreederId, FreeLimit, UsedCount FROM Benefits;
```

To avoid lock conflicts while the API is running, use read-only mode:

```bash
sqlite3 -readonly ca.db "SELECT * FROM Benefits;"
```

## Seed Data for Testing

`DbSeeder` provisions ready-made scenarios to verify limit behavior:

| BreederId | Scenario | FreeLimit | Litters |
|---|---|---|---|
| `11111111-1111-1111-1111-111111111111` | Limit not reached | 3 | Approved, Draft |
| `33333333-3333-3333-3333-333333333333` | Limit exhausted (`UsedCount` = 3) | 3 | Approved |
| `44444444-4444-4444-4444-444444444444` | No `BreederBenefit` record | — | Approved |

## API Endpoints

### Litters (`/api/litters`)

| Method | Endpoint | Headers | Description |
|---|---|---|---|
| POST | `/{litterId}/publish` | `X-Breeder-id` | Publish a litter (respects the `BreederBenefit` limit) |
| GET | `/` | `X-Breeder-id` | Get a breeder's litters (filterable/paginated via query) |