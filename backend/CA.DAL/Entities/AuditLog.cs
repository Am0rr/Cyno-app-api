using CA.DAL.Exceptions;

namespace CA.DAL.Entities;

public class AuditLog : BaseEntity
{
    public Guid EntityId { get; private set; }
    public string Action { get; private set; } = null!;

    protected AuditLog() { }

    public AuditLog(Guid entityId, string action)
    {
        if (string.IsNullOrWhiteSpace(action))
            throw new DomainException("Action cannot be empty.");

        EntityId = entityId;
        Action = action;
    }
}