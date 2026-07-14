using CA.DAL.Entities;
using CA.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace CA.DAL.Persistence.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Benefits.AnyAsync()) return;

        var breederWithLimit = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var breederLimitExceeded = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var otherBreeder = Guid.Parse("44444444-4444-4444-4444-444444444444");

        context.Benefits.AddRange(
            new BreederBenefit(breederWithLimit, freeLimit: 3),
            new BreederBenefit(breederLimitExceeded, freeLimit: 3)
        );

        context.Litters.AddRange(
            new Litter(breederWithLimit, LitterStatus.Approved),
            new Litter(breederWithLimit, LitterStatus.Draft),
            new Litter(otherBreeder, LitterStatus.Approved),
            new Litter(breederLimitExceeded, LitterStatus.Approved)
        );

        await context.SaveChangesAsync();

        var exceeded = await context.Benefits.FirstAsync(b => b.BreederId == breederLimitExceeded);
        exceeded.EnlargeUsedCount(3);
        await context.SaveChangesAsync();
    }
}