using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Common;

public abstract class AuditableEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : AuditableEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        // common stuff
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Created)
               .IsRequired();
        builder.Property(e => e.CreatedBy);
        builder.Property(e => e.LastModified);
        builder.Property(e => e.LastModifiedBy);

        // let child configs add their own rules
        ConfigureEntity(builder);
    }

    // to be implemented by child
    protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}
