using Domain.Entities;
using Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;
public class CorruptionReportConfiguration : AuditableEntityConfiguration<CorruptionReport>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CorruptionReport> builder)
    {
        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .IsRequired();

        builder.Property(r => r.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(r => r.ReporterId)
            .IsRequired();
    }
}