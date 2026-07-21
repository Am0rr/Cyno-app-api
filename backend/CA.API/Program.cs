using CA.API.Context;
using CA.API.Middleware;
using CA.BLL;
using CA.BLL.Interfaces;
using CA.DAL;
using CA.DAL.Persistence;
using CA.DAL.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddDataAccess(builder.Configuration)
    .AddApplication()
    .AddHttpContextAccessor()
    .AddScoped<ICurrentBreederContext, HttpCurrentBreederContext>();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseMiddleware<GlobalExceptionHandler>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Starting database migration...");
        await context.Database.MigrateAsync();
        logger.LogInformation("Database migrated successfully.");

        await DbSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        logger.LogCritical(ex, "An error occurred while migrating the database.");
        throw;
    }
}

app.MapControllers();

app.Run();