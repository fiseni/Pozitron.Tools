namespace Pozitron.SharedKernel;

public class AuditableEntity : IAuditableEntity
{
    public DateTimeOffset? AuditCreatedTime { get; private set; }
    public string? AuditCreatedByUserId { get; private set; }
    public string? AuditCreatedByUsername { get; private set; }
    public DateTimeOffset? AuditModifiedTime { get; private set; }
    public string? AuditModifiedByUserId { get; private set; }
    public string? AuditModifiedByUsername { get; private set; }

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
