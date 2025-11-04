using Domain.Entities;
using Domain.Enums;
using System.Reflection;

namespace Tests.Domain.Entities;

public class UserTest
{
    private const string ValidUsername = "johndoe";
    private const string ValidEmail = "john.doe@example.com";
    private const string ValidPasswordHash = "hashed_password_123";

    [SetUp]
    public void Setup()
    {
    }

    #region Constructor Tests

    [Test]
    public void Constructor_WithValidParameters_CreatesUser()
    {
        // Act
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.Username, Is.EqualTo(ValidUsername));
            Assert.That(user.Email, Is.EqualTo(ValidEmail.ToLowerInvariant()));
            Assert.That(user.PasswordHash, Is.EqualTo(ValidPasswordHash));
            Assert.That(user.Role, Is.EqualTo(UserRole.Citizen));
            Assert.That(user.Reports, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithSpecifiedRole_CreatesUserWithRole()
    {
        // Act
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash, UserRole.Investigator);

        // Assert
        Assert.That(user.Role, Is.EqualTo(UserRole.Investigator));
    }

    [Test]
    public void Constructor_WithWhitespaceInUsername_TrimsUsername()
    {
        // Arrange
        var usernameWithSpaces = "  johndoe  ";

        // Act
        var user = new User(usernameWithSpaces, ValidEmail, ValidPasswordHash);

        // Assert
        Assert.That(user.Username, Is.EqualTo("johndoe"));
    }

    [Test]
    public void Constructor_WithUppercaseEmail_ConvertsToLowercase()
    {
        // Arrange
        var uppercaseEmail = "John.Doe@EXAMPLE.COM";

        // Act
        var user = new User(ValidUsername, uppercaseEmail, ValidPasswordHash);

        // Assert
        Assert.That(user.Email, Is.EqualTo("john.doe@example.com"));
    }

    [Test]
    public void Constructor_WithWhitespaceInEmail_TrimsAndConvertsToLowercase()
    {
        // Arrange
        var emailWithSpaces = "  John.Doe@Example.com  ";

        // Act
        var user = new User(ValidUsername, emailWithSpaces, ValidPasswordHash);

        // Assert
        Assert.That(user.Email, Is.EqualTo("john.doe@example.com"));
    }

    [Test]
    public void Constructor_WithNullUsername_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(null!, ValidEmail, ValidPasswordHash));

        Assert.That(ex!.ParamName, Is.EqualTo("username"));
        Assert.That(ex.Message, Does.Contain("Username is required"));
    }

    [Test]
    public void Constructor_WithEmptyUsername_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(string.Empty, ValidEmail, ValidPasswordHash));

        Assert.That(ex!.ParamName, Is.EqualTo("username"));
        Assert.That(ex.Message, Does.Contain("Username is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceUsername_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User("   ", ValidEmail, ValidPasswordHash));

        Assert.That(ex!.ParamName, Is.EqualTo("username"));
    }

    [Test]
    public void Constructor_WithUsernameLessThan3Characters_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User("ab", ValidEmail, ValidPasswordHash));

        Assert.That(ex!.ParamName, Is.EqualTo("username"));
        Assert.That(ex.Message, Does.Contain("Username must be at least 3 characters long"));
    }

    [Test]
    public void Constructor_WithUsername3Characters_DoesNotThrow()
    {
        // Act & Assert
        Assert.DoesNotThrow(() => new User("abc", ValidEmail, ValidPasswordHash));
    }

    [Test]
    public void Constructor_WithNullEmail_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(ValidUsername, null!, ValidPasswordHash));

        Assert.That(ex!.ParamName, Is.EqualTo("email"));
        Assert.That(ex.Message, Does.Contain("Email is required"));
    }

    [Test]
    public void Constructor_WithEmptyEmail_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(ValidUsername, string.Empty, ValidPasswordHash));

        Assert.That(ex!.ParamName, Is.EqualTo("email"));
    }

    [Test]
    public void Constructor_WithWhitespaceEmail_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(ValidUsername, "   ", ValidPasswordHash));

        Assert.That(ex!.ParamName, Is.EqualTo("email"));
    }

    [TestCase("invalidemail")]
    [TestCase("invalid@")]
    [TestCase("@invalid.com")]
    [TestCase("invalid@domain")]
    [TestCase("invalid domain@example.com")]
    [TestCase("invalid@domain .com")]
    public void Constructor_WithInvalidEmailFormat_ThrowsArgumentException(string invalidEmail)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(ValidUsername, invalidEmail, ValidPasswordHash));

        Assert.That(ex!.ParamName, Is.EqualTo("email"));
        Assert.That(ex.Message, Does.Contain("Invalid email format"));
    }

    [TestCase("user@example.com")]
    [TestCase("user.name@example.com")]
    [TestCase("user+tag@example.co.uk")]
    [TestCase("user123@sub.example.com")]
    public void Constructor_WithValidEmailFormats_DoesNotThrow(string validEmail)
    {
        // Act & Assert
        Assert.DoesNotThrow(() => new User(ValidUsername, validEmail, ValidPasswordHash));
    }

    [Test]
    public void Constructor_WithNullPasswordHash_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(ValidUsername, ValidEmail, null!));

        Assert.That(ex!.ParamName, Is.EqualTo("passwordHash"));
        Assert.That(ex.Message, Does.Contain("Password hash is required"));
    }

    [Test]
    public void Constructor_WithEmptyPasswordHash_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(ValidUsername, ValidEmail, string.Empty));

        Assert.That(ex!.ParamName, Is.EqualTo("passwordHash"));
    }

    [Test]
    public void Constructor_WithWhitespacePasswordHash_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            new User(ValidUsername, ValidEmail, "   "));

        Assert.That(ex!.ParamName, Is.EqualTo("passwordHash"));
    }

    #endregion

    #region ChangeRole Tests

    [TestCase(UserRole.Citizen)]
    [TestCase(UserRole.Investigator)]
    [TestCase(UserRole.DepartmentHead)]
    [TestCase(UserRole.Admin)]
    [TestCase(UserRole.SuperAdmin)]
    public void ChangeRole_WithValidRole_UpdatesRole(UserRole newRole)
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);

        // Act
        user.ChangeRole(newRole);

        // Assert
        Assert.That(user.Role, Is.EqualTo(newRole));
    }

    [Test]
    public void ChangeRole_WithValidRole_MarksEntityAsModified()
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);

        // Act
        user.ChangeRole(UserRole.Admin);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.LastModified, Is.Not.Null);
            Assert.That(user.LastModifiedBy, Is.EqualTo(user.Id));
        });
    }

    [Test]
    public void ChangeRole_WithInvalidRole_ThrowsArgumentException()
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);
        var invalidRole = (UserRole)999;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => user.ChangeRole(invalidRole));

        Assert.That(ex!.ParamName, Is.EqualTo("newRole"));
        Assert.That(ex.Message, Does.Contain("Invalid role"));
    }

    [Test]
    public void ChangeRole_FromCitizenToInvestigator_UpdatesRole()
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash, UserRole.Citizen);

        // Act
        user.ChangeRole(UserRole.Investigator);

        // Assert
        Assert.That(user.Role, Is.EqualTo(UserRole.Investigator));
    }

    #endregion

    #region AddReport Tests

    [Test]
    public void AddReport_WithValidReport_AddsReportToCollection()
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);
        SetUserId(user, Guid.NewGuid());
        
        var report = new CorruptionReport(
            "Test Report",
            "Test Description",
            user.Id,
            DateTime.UtcNow.AddDays(-1));

        // Act
        user.AddReport(report);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(user.Reports, Has.Count.EqualTo(1));
            Assert.That(user.Reports, Contains.Item(report));
        });
    }

    [Test]
    public void AddReport_WithMultipleReports_AddsAllReports()
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);
        SetUserId(user, Guid.NewGuid());

        var report1 = new CorruptionReport("Report 1", "Description 1", user.Id, DateTime.UtcNow.AddDays(-1));
        var report2 = new CorruptionReport("Report 2", "Description 2", user.Id, DateTime.UtcNow.AddDays(-2));

        // Act
        user.AddReport(report1);
        user.AddReport(report2);

        // Assert
        Assert.That(user.Reports, Has.Count.EqualTo(2));
    }

    [Test]
    public void AddReport_WithNullReport_ThrowsArgumentNullException()
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => user.AddReport(null!));

        Assert.That(ex!.ParamName, Is.EqualTo("report"));
    }

    [Test]
    public void AddReport_WithReportBelongingToDifferentUser_ThrowsInvalidOperationException()
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);
        SetUserId(user, Guid.NewGuid());

        var differentUserId = Guid.NewGuid();
        var report = new CorruptionReport(
            "Test Report",
            "Test Description",
            differentUserId,
            DateTime.UtcNow.AddDays(-1));

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => user.AddReport(report));

        Assert.That(ex!.Message, Does.Contain("Report does not belong to this user"));
    }

    [Test]
    public void Reports_ReturnsReadOnlyCollection()
    {
        // Arrange
        var user = new User(ValidUsername, ValidEmail, ValidPasswordHash);

        // Act
        var reports = user.Reports;

        // Assert
        Assert.That(reports, Is.InstanceOf<IReadOnlyCollection<CorruptionReport>>());
    }

    #endregion

    #region Helper Methods

    private static void SetUserId(User user, Guid id)
    {
        // Use reflection to set the Id property from AuditableEntity
        var idProperty = typeof(User).BaseType!
            .GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
        
        idProperty!.SetValue(user, id);
    }

    #endregion
}
