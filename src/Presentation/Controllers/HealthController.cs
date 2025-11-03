using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public HealthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Status = "Healthy",
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            Timestamp = DateTime.UtcNow,
            Message = "Civic Integrity Hub API is running"
        });
    }

    [HttpGet("connection")]
    public IActionResult GetConnectionString()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        // Mask password for security
        var maskedConnectionString = connectionString?.Replace(
            connectionString.Split("Password=")[1].Split(";")[0],
            "****"
        );

        return Ok(new
        {
            ConnectionString = maskedConnectionString,
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        });
    }
}