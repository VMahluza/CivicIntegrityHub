using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Persistence.Configurations;

public class CorruptionReportConfigurationTest
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

    #region CorruptionReport Configuration Tests

    [Test]
    public void CorruptionReportConfiguration_TitleProperty_IsMappedAndRequired()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var titleProperty = entityType!.FindProperty(nameof(CorruptionReport.Title));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(titleProperty, Is.Not.Null, "Title property should be mapped");
            Assert.That(titleProperty!.IsNullable, Is.False, "Title property should be required");
        });
    }

    [Test]
    public void CorruptionReportConfiguration_TitleProperty_HasMaxLength200()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var titleProperty = entityType!.FindProperty(nameof(CorruptionReport.Title));
        var maxLength = titleProperty!.GetMaxLength();

        // Assert
        Assert.That(maxLength, Is.EqualTo(200), "Title should have max length of 200");
    }

    [Test]
    public void CorruptionReportConfiguration_DescriptionProperty_IsMappedAndRequired()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var descriptionProperty = entityType!.FindProperty(nameof(CorruptionReport.Description));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(descriptionProperty, Is.Not.Null, "Description property should be mapped");
            Assert.That(descriptionProperty!.IsNullable, Is.False, "Description property should be required");
        });
    }

    [Test]
    public void CorruptionReportConfiguration_StatusProperty_IsMappedAndRequired()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var statusProperty = entityType!.FindProperty(nameof(CorruptionReport.Status));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(statusProperty, Is.Not.Null, "Status property should be mapped");
            Assert.That(statusProperty!.IsNullable, Is.False, "Status property should be required");
            Assert.That(statusProperty.ClrType, Is.EqualTo(typeof(ReportStatus)), 
                "Status should be of type ReportStatus");
        });
    }

    [Test]
    public void CorruptionReportConfiguration_ReporterIdProperty_IsMappedAndRequired()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var reporterIdProperty = entityType!.FindProperty(nameof(CorruptionReport.ReporterId));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reporterIdProperty, Is.Not.Null, "ReporterId property should be mapped");
            Assert.That(reporterIdProperty!.IsNullable, Is.False, "ReporterId property should be required");
        });
    }

    [Test]
    public void CorruptionReportConfiguration_InheritsAuditableProperties()
    {
        // Arrange
        var entityType = _context.Model.FindEntityType(typeof(CorruptionReport));

        // Act
        var createdProperty = entityType!.FindProperty(nameof(CorruptionReport.Created));
        var createdByProperty = entityType.FindProperty(nameof(CorruptionReport.CreatedBy));
        var lastModifiedProperty = entityType.FindProperty(nameof(CorruptionReport.LastModified));
        var lastModifiedByProperty = entityType.FindProperty(nameof(CorruptionReport.LastModifiedBy));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(createdProperty, Is.Not.Null, "Created property should be inherited and mapped");
            Assert.That(createdByProperty, Is.Not.Null, "CreatedBy property should be inherited and mapped");
            Assert.That(lastModifiedProperty, Is.Not.Null, "LastModified property should be inherited and mapped");
            Assert.That(lastModifiedByProperty, Is.Not.Null, "LastModifiedBy property should be inherited and mapped");
        });
    }

    [Test]
    public void CorruptionReportConfiguration_CanSaveAndRetrieveEntity()
    {
        // Arrange
        var reporterId = Guid.NewGuid();
        var report = new CorruptionReport(
            "Corruption in Public Tender",
            "Observed irregularities in the bidding process",
            reporterId,
            DateTime.UtcNow.AddDays(-1)
        );

        // Act
        _context.Set<CorruptionReport>().Add(report);
        _context.SaveChanges();

        var retrievedReport = _context.Set<CorruptionReport>().FirstOrDefault(r => r.Id == report.Id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(retrievedReport, Is.Not.Null, "Report should be retrieved from database");
            Assert.That(retrievedReport!.Title, Is.EqualTo("Corruption in Public Tender"));
            Assert.That(retrievedReport.Description, Is.EqualTo("Observed irregularities in the bidding process"));
            Assert.That(retrievedReport.ReporterId, Is.EqualTo(reporterId));
            Assert.That(retrievedReport.Status, Is.EqualTo(ReportStatus.Submitted));
        });
    }

    [Test]
    public void CorruptionReportConfiguration_StatusConversion_SavesAsString()
    {
        // Arrange
        var reporterId = Guid.NewGuid();
        var report = new CorruptionReport(
            "Test Report",
            "Test Description",
            reporterId,
            DateTime.UtcNow
        );
        report.ChangeStatus(ReportStatus.UnderInvestigation);

        // Act
        _context.Set<CorruptionReport>().Add(report);
        _context.SaveChanges();

        var retrievedReport = _context.Set<CorruptionReport>().FirstOrDefault(r => r.Id == report.Id);

        // Assert
        Assert.That(retrievedReport!.Status, Is.EqualTo(ReportStatus.UnderInvestigation), 
            "Status should be correctly converted and retrieved");
    }

    #endregion
}