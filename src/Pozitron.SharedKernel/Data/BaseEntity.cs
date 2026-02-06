namespace Pozitron.SharedKernel;

public abstract class BaseEntity : BaseEntity<Guid>
{
}

public abstract class BaseEntity<T> : IEntity<T>, IAuditableEntity, IDomainEventContainer, ISoftDelete where T : struct
{
    public T Id { get; protected set; }
    public bool IsDeleted { get; private set; }

    public DateTimeOffset? AuditCreatedTime { get; private set; }
    public string? AuditCreatedByUserId { get; private set; }
    public string? AuditCreatedByUsername { get; private set; }
    public DateTimeOffset? AuditModifiedTime { get; private set; }
    public string? AuditModifiedByUserId { get; private set; }
    public string? AuditModifiedByUsername { get; private set; }

    private readonly List<DomainEvent> _events = new();
    public IEnumerable<DomainEvent> Events => _events.AsEnumerable();

    public void ClearDomainEvents() => _events.Clear();

    public void RegisterEvent(DomainEvent domainEvent) => _events.Add(domainEvent);

    public void UpdateCreateInfo(DateTimeOffset now, ICurrentUser currentUser)
    {
        AuditCreatedTime = now;
        AuditCreatedByUserId = currentUser?.UserId;
        AuditCreatedByUsername = currentUser?.Username;
    }

    public void UpdateModifyInfo(DateTimeOffset now, ICurrentUser currentUser)
    {
        AuditModifiedTime = now;
        AuditModifiedByUserId = currentUser?.UserId;
        AuditModifiedByUsername = currentUser?.Username;
    }
}
