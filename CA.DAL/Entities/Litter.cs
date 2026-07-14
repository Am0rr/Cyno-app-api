using CA.DAL.Enums;

namespace CA.DAL.Entities;

public class Litter : BaseEntity
{
    public Guid BreederId { get; private set; }
    public LitterStatus Status { get; private set; } = LitterStatus.Draft;

    protected Litter() { }

    public Litter(Guid breederId)
    {
        BreederId = breederId;
    }
}