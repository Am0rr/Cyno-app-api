using CA.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CA.DAL.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Litter> Litters { get; set; }
    public DbSet<BreederBenefit> Benefits { get; set; }
    public DbSet<AuditLog> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}