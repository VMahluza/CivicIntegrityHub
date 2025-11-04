using Domain.Entities.Common;
using Domain.Enums;
namespace Domain.Entities;
public class CorruptionReport : AuditableEntity
{
    public string Title { get; private set; } = null!;
    public string Description { get;private set; } = null!;
    public Guid ReporterId { get; private set; }
    public ReportStatus Status { get; set; } = ReportStatus.Draft;

    public DateTime OccurredOn { get; private set; }
    private CorruptionReport() { } // for EF

    public CorruptionReport(
        string title,
        string description,
        Guid reporterId,
        DateTime occurredOn)
    {
        // --- Guard clauses ---
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        if (occurredOn > DateTime.UtcNow)
            throw new ArgumentException("Occurred date cannot be in the future.", nameof(occurredOn));

        if (reporterId == Guid.Empty)
            throw new ArgumentException("Reporter ID is required.", nameof(reporterId));

        // --- Assignments ---
        Title = title.Trim();
        Description = description.Trim();
        ReporterId = reporterId;
        OccurredOn = occurredOn;
        Status = ReportStatus.Submitted;  // start as submitted after creation
    }

    // --- Domain behaviors ---
    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Description cannot be empty.", nameof(newDescription));

        Description = newDescription.Trim();
        MarkModified(ReporterId);
    }

    public void ChangeStatus(ReportStatus newStatus)
    {
        // later: add validation rules (e.g., cannot move backward)
        Status = newStatus;
        MarkModified(ReporterId);
    }

}