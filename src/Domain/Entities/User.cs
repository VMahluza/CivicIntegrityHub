using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;
public class User : AuditableEntity
{
    public string Username { get; set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.Citizen;

    private User() { } // for EF

}
