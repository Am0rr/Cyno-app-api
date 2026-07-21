using CA.DAL.Enums;

namespace CA.DAL.Entities;

public class Litter : BaseEntity
{
    public Guid BreederId { get; private set; }
    public LitterStatus Status { get; private set; }

    protected Litter() { }

    public Litter(Guid breederId, LitterStatus status = LitterStatus.Draft)
    {
        BreederId = breederId;
        Status = status;
    }

    public void ChangeStatus(LitterStatus newStatus)
    {
        Status = newStatus;
    }
}