namespace Domain.Entities.Common;
public abstract class AuditableEntity
{
    public Guid Id { get; private set; }
    public DateTime Created { get; set; }
    public Guid CreatedBy { get; set; } = Guid.NewGuid();
    public DateTime? LastModified { get; set; }
    public Guid? LastModifiedBy { get; set; }

    protected AuditableEntity() { }

    public void MarkCreated(Guid createdBy)
    {
        Created = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public void MarkModified(Guid modifiedBy)
    {
        LastModified = DateTime.UtcNow;
        LastModifiedBy = modifiedBy;
    }
}
