using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
// MySQL connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CivicDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable Swagger in all environments for testing (remove condition)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add a root endpoint
app.MapGet("/", () => Results.Ok(new
{
    Service = "Civic Integrity Hub API",
    Version = "1.0.0",
    Status = "Running",
    Endpoints = new[]
    {
        "/swagger",
        "/api/health",
        "/api/health/connection"
    }
}));

app.Run();
