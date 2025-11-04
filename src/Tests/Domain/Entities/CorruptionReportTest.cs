using Domain.Entities;
using Domain.Enums;
namespace Tests.Domain.Entities;
public class CorruptionReportTest
{
    private Guid _validReporterId;
    private DateTime _validOccurredOn;
    private const string ValidTitle = "Corruption in Public Tender";
    private const string ValidDescription = "Observed irregularities in the bidding process";

    [SetUp]
    public void Setup()
    {
        _validReporterId = Guid.NewGuid();
        _validOccurredOn = DateTime.UtcNow.AddDays(-1);
    }

    #region Constructor Tests

    [Test]
    public void Constructor_WithValidParameters_CreatesCorruptionReport()
    {
        // Act
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.Title, Is.EqualTo(ValidTitle));
            Assert.That(report.Description, Is.EqualTo(ValidDescription));
            Assert.That(report.ReporterId, Is.EqualTo(_validReporterId));
            Assert.That(report.OccurredOn, Is.EqualTo(_validOccurredOn));
            Assert.That(report.Status, Is.EqualTo(ReportStatus.Submitted));
        });
    }

    [Test]
    public void Constructor_WithWhitespaceInTitleAndDescription_TrimsValues()
    {
        // Arrange
        var titleWithSpaces = "  Corruption Report  ";
        var descriptionWithSpaces = "  Some description  ";

        // Act
        var report = new CorruptionReport(titleWithSpaces, descriptionWithSpaces, _validReporterId, _validOccurredOn);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.Title, Is.EqualTo("Corruption Report"));
            Assert.That(report.Description, Is.EqualTo("Some description"));
        });
    }

    [Test]
    public void Constructor_WithNullTitle_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => 
            new CorruptionReport(null!, ValidDescription, _validReporterId, _validOccurredOn));
        
        Assert.That(ex!.ParamName, Is.EqualTo("title"));
        Assert.That(ex.Message, Does.Contain("Title is required"));
    }

    [Test]
    public void Constructor_WithEmptyTitle_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => 
            new CorruptionReport(string.Empty, ValidDescription, _validReporterId, _validOccurredOn));
        
        Assert.That(ex!.ParamName, Is.EqualTo("title"));
        Assert.That(ex.Message, Does.Contain("Title is required"));
    }

    [Test]
    public void Constructor_WithWhitespaceTitle_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => 
            new CorruptionReport("   ", ValidDescription, _validReporterId, _validOccurredOn));
        
        Assert.That(ex!.ParamName, Is.EqualTo("title"));
    }

    [Test]
    public void Constructor_WithNullDescription_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => 
            new CorruptionReport(ValidTitle, null!, _validReporterId, _validOccurredOn));
        
        Assert.That(ex!.ParamName, Is.EqualTo("description"));
        Assert.That(ex.Message, Does.Contain("Description is required"));
    }

    [Test]
    public void Constructor_WithEmptyDescription_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => 
            new CorruptionReport(ValidTitle, string.Empty, _validReporterId, _validOccurredOn));
        
        Assert.That(ex!.ParamName, Is.EqualTo("description"));
    }

    [Test]
    public void Constructor_WithEmptyGuid_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => 
            new CorruptionReport(ValidTitle, ValidDescription, Guid.Empty, _validOccurredOn));
        
        Assert.That(ex!.ParamName, Is.EqualTo("reporterId"));
        Assert.That(ex.Message, Does.Contain("Reporter ID is required"));
    }

    [Test]
    public void Constructor_WithFutureOccurredDate_ThrowsArgumentException()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(1);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => 
            new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, futureDate));
        
        Assert.That(ex!.ParamName, Is.EqualTo("occurredOn"));
        Assert.That(ex.Message, Does.Contain("Occurred date cannot be in the future"));
    }

    [Test]
    public void Constructor_WithCurrentDate_DoesNotThrow()
    {
        // Arrange
        var currentDate = DateTime.UtcNow;

        // Act & Assert
        Assert.DoesNotThrow(() => 
            new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, currentDate));
    }

    #endregion

    #region UpdateDescription Tests

    [Test]
    public void UpdateDescription_WithValidDescription_UpdatesDescription()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);
        var newDescription = "Updated corruption details";

        // Act
        report.UpdateDescription(newDescription);

        // Assert
        Assert.That(report.Description, Is.EqualTo(newDescription));
    }

    [Test]
    public void UpdateDescription_WithWhitespace_TrimsDescription()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);
        var newDescription = "  Updated description  ";

        // Act
        report.UpdateDescription(newDescription);

        // Assert
        Assert.That(report.Description, Is.EqualTo("Updated description"));
    }

    [Test]
    public void UpdateDescription_WithValidDescription_MarksEntityAsModified()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);
        var newDescription = "Updated description";

        // Act
        report.UpdateDescription(newDescription);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.LastModified, Is.Not.Null);
            Assert.That(report.LastModifiedBy, Is.EqualTo(_validReporterId));
        });
    }

    [Test]
    public void UpdateDescription_WithNullDescription_ThrowsArgumentException()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => report.UpdateDescription(null!));
        
        Assert.That(ex!.ParamName, Is.EqualTo("newDescription"));
        Assert.That(ex.Message, Does.Contain("Description cannot be empty"));
    }

    [Test]
    public void UpdateDescription_WithEmptyDescription_ThrowsArgumentException()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => report.UpdateDescription(string.Empty));
        
        Assert.That(ex!.ParamName, Is.EqualTo("newDescription"));
    }

    [Test]
    public void UpdateDescription_WithWhitespaceOnly_ThrowsArgumentException()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => report.UpdateDescription("   "));
    }

    #endregion

    #region ChangeStatus Tests

    [Test]
    public void ChangeStatus_WithValidStatus_UpdatesStatus()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);

        // Act
        report.ChangeStatus(ReportStatus.UnderInvestigation);

        // Assert
        Assert.That(report.Status, Is.EqualTo(ReportStatus.UnderInvestigation));
    }

    [Test]
    public void ChangeStatus_WithValidStatus_MarksEntityAsModified()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);

        // Act
        report.ChangeStatus(ReportStatus.UnderInvestigation);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.LastModified, Is.Not.Null);
            Assert.That(report.LastModifiedBy, Is.EqualTo(_validReporterId));
        });
    }

    [Test]
    public void ChangeStatus_ToResolved_UpdatesStatus()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);

        // Act
        report.ChangeStatus(ReportStatus.Resolved);

        // Assert
        Assert.That(report.Status, Is.EqualTo(ReportStatus.Resolved));
    }

    [Test]
    public void ChangeStatus_ToRejected_UpdatesStatus()
    {
        // Arrange
        var report = new CorruptionReport(ValidTitle, ValidDescription, _validReporterId, _validOccurredOn);

        // Act
        report.ChangeStatus(ReportStatus.Dismissed);

        // Assert
        Assert.That(report.Status, Is.EqualTo(ReportStatus.Dismissed));
    }

    #endregion
}