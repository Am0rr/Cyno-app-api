using CA.DAL.Exceptions;

namespace CA.DAL.Entities;

public class BreederBenefit
{
    public Guid BreederId { get; private set; }
    public int FreeLimit { get; private set; }
    public int UsedCount { get; private set; } = 0;

    protected BreederBenefit() { }

    public BreederBenefit(Guid breederId, int freeLimit)
    {
        if (freeLimit < 0)
            throw new DomainException("FreeLimit cannot be negative.");

        BreederId = breederId;
        FreeLimit = freeLimit;
    }
}