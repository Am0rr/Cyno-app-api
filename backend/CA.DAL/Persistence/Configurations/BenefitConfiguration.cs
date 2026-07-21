using CA.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CA.DAL.Persistence.Configurations;

public class BenefitConfiguration : IEntityTypeConfiguration<BreederBenefit>
{
    public void Configure(EntityTypeBuilder<BreederBenefit> builder)
    {
        builder.ToTable("Benefits");

        builder.HasKey(l => l.BreederId);

        builder.Property(b => b.FreeLimit).IsRequired();
        builder.Property(b => b.UsedCount).IsRequired();
        builder.Property(b => b.RowVersion).IsRowVersion();
    }
}