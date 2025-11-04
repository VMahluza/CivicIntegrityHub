namespace Domain.Enums;
public enum UserRole
{
    Citizen = 0,          // regular user who submits reports
    Investigator = 1,     // reviews and investigates reports
    DepartmentHead = 2,   // oversees reports in their department
    Admin = 3,            // manages categories, departments, and users
    SuperAdmin = 4        // full system control (for rare cases)
}