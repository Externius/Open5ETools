namespace Open5ETools.Core.Domain;

public abstract class AuditableEntity : BaseEntity
{
    public required string CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public required string LastModifiedBy { get; set; }
    public DateTime LastModified { get; set; }
}