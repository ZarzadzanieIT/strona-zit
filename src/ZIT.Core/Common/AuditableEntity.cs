namespace ZIT.Core.Common;

public abstract class AuditableEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset? CreatedAt { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public DateTimeOffset? ModifiedAt { get; protected set; }
    public string? ModifiedBy { get; protected set; }
}