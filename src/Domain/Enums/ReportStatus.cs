namespace Domain.Enums;
public enum ReportStatus
{
    Draft = 0,              // Created but not yet submitted
    Submitted = 1,          // Sent in by citizen, waiting for review
    UnderInvestigation = 2, // Investigator assigned, case in progress
    Resolved = 3,           // Issue addressed or action taken
    Dismissed = 4           // Rejected or found invalid
}