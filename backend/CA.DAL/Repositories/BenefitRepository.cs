using CA.DAL.Entities;
using CA.DAL.Interfaces;
using CA.DAL.Persistence;

namespace CA.DAL.Repositories;

public class BenefitRepository(AppDbContext context) : IBenefitRepository
{
    private readonly AppDbContext _context = context;

    public async Task<BreederBenefit?> GetByBreederIdAsync(Guid breederId)
    {
        return await _context.Benefits.FindAsync(breederId);
    }
}