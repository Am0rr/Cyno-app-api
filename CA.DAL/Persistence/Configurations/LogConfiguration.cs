using CA.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CA.DAL.Persistence.Configurations;

public class LogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("Logs");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.EntityId).IsRequired();
        builder.Property(l => l.Action)
            .IsRequired()
            .HasMaxLength(200);
    }
}