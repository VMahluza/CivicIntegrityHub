using Domain.Entities.Common;
using Domain.Enums;
using System.Text.RegularExpressions;

namespace Domain.Entities;
public class User : AuditableEntity
{
    public string Username { get;private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; } = UserRole.Citizen;

    private readonly List<CorruptionReport> _reports = new();
    public IReadOnlyCollection<CorruptionReport> Reports => _reports.AsReadOnly();
    private User() { } // for EF


    public User(string username, string email, string passwordHash, UserRole role = UserRole.Citizen)
    {
        // --- Guard Clauses ---
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required.", nameof(username));

        if (username.Length < 3)
            throw new ArgumentException("Username must be at least 3 characters long.", nameof(username));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        // Trim email before validation
        email = email.Trim();

        // simple email format check
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format.", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required.", nameof(passwordHash));

        // --- Assignments ---
        Username = username.Trim();
        Email = email.ToLowerInvariant();
        PasswordHash = passwordHash;
        Role = role;
    }

    // --- Domain Behavior (optional examples) ---
    public void ChangeRole(UserRole newRole)
    {
        if (!Enum.IsDefined(typeof(UserRole), newRole))
            throw new ArgumentException("Invalid role.", nameof(newRole));

        Role = newRole;
        MarkModified(Id); // self-modified
    }

    public void AddReport(CorruptionReport report)
    {
        if (report == null)
            throw new ArgumentNullException(nameof(report));

        if (report.ReporterId != Id)
            throw new InvalidOperationException("Report does not belong to this user.");

        _reports.Add(report);
    }
}
