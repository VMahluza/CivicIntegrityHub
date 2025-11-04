using Domain.Entities.Common;
using Domain.Enums;
namespace Domain.Entities;
public class CorruptionReport : AuditableEntity
{
    public string Title { get; private set; } = null!;
    public string Description { get;private set; } = null!;
    public Guid ReporterId { get; private set; }
    public ReportStatus Status { get; set; } = ReportStatus.Draft;
    private CorruptionReport() { } // for EF

}