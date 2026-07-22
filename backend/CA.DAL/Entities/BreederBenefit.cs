using CA.DAL.Exceptions;

namespace CA.DAL.Entities;

public class BreederBenefit
{
    public Guid BreederId { get; private set; }
    public int FreeLimit { get; private set; }
    public int UsedCount { get; private set; } = 0;
    public Guid RowVersion { get; private set; } = Guid.NewGuid();

    protected BreederBenefit() { }

    public BreederBenefit(Guid breederId, int freeLimit)
    {
        if (freeLimit < 0)
            throw new DomainException("FreeLimit cannot be negative.");

        BreederId = breederId;
        FreeLimit = freeLimit;
    }

    public void EnlargeUsedCount(int count)
    {
        if (count <= 0)
            throw new DomainException("Count must be positive.");

        if (UsedCount + count > FreeLimit)
            throw new DomainException("Cannot exceed free publication limit.");

        UsedCount += count;
        RowVersion = Guid.NewGuid();
    }
}