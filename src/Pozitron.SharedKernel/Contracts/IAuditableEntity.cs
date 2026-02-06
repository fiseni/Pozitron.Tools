namespace Pozitron.SharedKernel;

public interface IAuditableEntity
{
    DateTimeOffset? AuditCreatedTime { get; }
    string? AuditCreatedByUserId { get; }
    string? AuditCreatedByUsername { get; }
    DateTimeOffset? AuditModifiedTime { get; }
    string? AuditModifiedByUserId { get; }
    string? AuditModifiedByUsername { get; }

    void UpdateCreateInfo(DateTimeOffset now, ICurrentUser currentUser);
    void UpdateModifyInfo(DateTimeOffset now, ICurrentUser currentUser);
}

