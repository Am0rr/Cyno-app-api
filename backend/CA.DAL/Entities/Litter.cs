using CA.DAL.Enums;

namespace CA.DAL.Entities;

public class Litter : BaseEntity
{
    public Guid BreederId { get; private set; }
    public LitterStatus Status { get; private set; }
    public Guid RowVersion { get; private set; } = Guid.NewGuid();

    protected Litter() { }

    public Litter(Guid breederId, LitterStatus status = LitterStatus.Draft)
    {
        BreederId = breederId;
        Status = status;
    }

    public void ChangeStatus(LitterStatus newStatus)
    {
        Status = newStatus;
        RowVersion = Guid.NewGuid();
    }
}