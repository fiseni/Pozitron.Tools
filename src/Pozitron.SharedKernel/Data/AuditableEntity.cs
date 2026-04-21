namespace Pozitron.SharedKernel;

public class AuditableEntity : IAuditableEntity
{
    public DateTimeOffset? AuditCreatedTime { get; private set; }
    public string? AuditCreatedByUserId { get; private set; }
    public string? AuditCreatedByUsername { get; private set; }
    public DateTimeOffset? AuditModifiedTime { get; private set; }
    public string? AuditModifiedByUserId { get; private set; }
    public string? AuditModifiedByUsername { get; private set; }
}
