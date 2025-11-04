namespace Domain.Entities.Common;
public abstract class AuditableEntity
{
    public Guid Id { get; private set; }
    public DateTime Created { get; private set; }
    public Guid CreatedBy { get; private set; } 
    public DateTime? LastModified { get; private set; }
    public Guid? LastModifiedBy { get; private set; }

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
