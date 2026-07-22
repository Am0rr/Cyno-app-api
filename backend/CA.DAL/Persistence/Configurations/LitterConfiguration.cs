using CA.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CA.DAL.Persistence.Configurations;

public class LitterConfiguration : IEntityTypeConfiguration<Litter>
{
    public void Configure(EntityTypeBuilder<Litter> builder)
    {
        builder.ToTable("Litters");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.CreatedAt).IsRequired();
        builder.Property(l => l.BreederId).IsRequired();
        builder.Property(l => l.Status).IsRequired().HasConversion<string>();
        builder.Property(l => l.RowVersion).IsConcurrencyToken();
    }
}