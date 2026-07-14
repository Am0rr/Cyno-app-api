using CA.DAL.Entities;

namespace CA.DAL.Interfaces;

public interface IBenefitRepository
{
    Task<BreederBenefit?> GetByBreederIdAsync(Guid breederId);
}