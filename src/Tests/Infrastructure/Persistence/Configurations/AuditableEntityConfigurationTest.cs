using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.InMemory; // Add this using directive

namespace Tests.Infrastructure.Persistence.Configurations;

public class AuditableEntityConfigurationTest
{
    private DbContextOptions<CivicDbContext> _options = null!;
    private CivicDbContext _context = null!;

    [SetUp]
    public void Setup()
    {
        // Use in-memory database for testing
        _options = new DbContextOptionsBuilder<CivicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CivicDbContext(_options);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    #region User Entity Configuration Tests

    [Test]
    public void UserConfiguration_IdProperty_IsMappedAndIsKey()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(User));

        // Act
        var idProperty = entityType!.FindProperty(nameof(User.Id));
        var primaryKey = entityType.FindPrimaryKey();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(idProperty, Is.Not.Null, "Id property should be mapped");
            Assert.That(primaryKey, Is.Not.Null, "Primary key should be defined");
            Assert.That(primaryKey!.Properties.Select(p => p.Name), Contains.Item(nameof(User.Id)), 
                "Id should be the primary key");
        });
    }

    [Test]
    public void UserConfiguration_CreatedProperty_IsMappedAndRequired()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(User));

        // Act
        var createdProperty = entityType!.FindProperty(nameof(User.Created));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(createdProperty, Is.Not.Null, "Created property should be mapped");
            Assert.That(createdProperty!.IsNullable, Is.False, "Created property should be required");
        });
    }

    [Test]
    public void UserConfiguration_CreatedByProperty_IsMapped()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(User));

        // Act
        var createdByProperty = entityType!.FindProperty(nameof(User.CreatedBy));

        // Assert
        Assert.That(createdByProperty, Is.Not.Null, "CreatedBy property should be mapped");
    }

    [Test]
    public void UserConfiguration_LastModifiedProperty_IsMappedAndNullable()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(User));

        // Act
        var lastModifiedProperty = entityType!.FindProperty(nameof(User.LastModified));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lastModifiedProperty, Is.Not.Null, "LastModified property should be mapped");
            Assert.That(lastModifiedProperty!.IsNullable, Is.True, "LastModified should be nullable");
        });
    }

    [Test]
    public void UserConfiguration_LastModifiedByProperty_IsMappedAndNullable()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(User));

        // Act
        var lastModifiedByProperty = entityType!.FindProperty(nameof(User.LastModifiedBy));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lastModifiedByProperty, Is.Not.Null, "LastModifiedBy property should be mapped");
            Assert.That(lastModifiedByProperty!.IsNullable, Is.True, "LastModifiedBy should be nullable");
        });
    }

    #endregion

    #region CorruptionReport Entity Configuration Tests

    [Test]
    public void CorruptionReportConfiguration_IdProperty_IsMappedAndIsKey()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var idProperty = entityType!.FindProperty(nameof(CorruptionReport.Id));
        var primaryKey = entityType.FindPrimaryKey();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(idProperty, Is.Not.Null, "Id property should be mapped");
            Assert.That(primaryKey, Is.Not.Null, "Primary key should be defined");
            Assert.That(primaryKey!.Properties.Select(p => p.Name), Contains.Item(nameof(CorruptionReport.Id)), 
                "Id should be the primary key");
        });
    }

    [Test]
    public void CorruptionReportConfiguration_CreatedProperty_IsMappedAndRequired()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var createdProperty = entityType!.FindProperty(nameof(CorruptionReport.Created));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(createdProperty, Is.Not.Null, "Created property should be mapped");
            Assert.That(createdProperty!.IsNullable, Is.False, "Created property should be required");
        });
    }

    [Test]
    public void CorruptionReportConfiguration_CreatedByProperty_IsMapped()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var createdByProperty = entityType!.FindProperty(nameof(CorruptionReport.CreatedBy));

        // Assert
        Assert.That(createdByProperty, Is.Not.Null, "CreatedBy property should be mapped");
    }

    [Test]
    public void CorruptionReportConfiguration_LastModifiedProperty_IsMappedAndNullable()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var lastModifiedProperty = entityType!.FindProperty(nameof(CorruptionReport.LastModified));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lastModifiedProperty, Is.Not.Null, "LastModified property should be mapped");
            Assert.That(lastModifiedProperty!.IsNullable, Is.True, "LastModified should be nullable");
        });
    }

    [Test]
    public void CorruptionReportConfiguration_LastModifiedByProperty_IsMappedAndNullable()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var lastModifiedByProperty = entityType!.FindProperty(nameof(CorruptionReport.LastModifiedBy));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(lastModifiedByProperty, Is.Not.Null, "LastModifiedBy property should be mapped");
            Assert.That(lastModifiedByProperty!.IsNullable, Is.True, "LastModifiedBy should be nullable");
        });
    }

    #endregion
}