using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class CivicDbContext(DbContextOptions<CivicDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<CorruptionReport> CorruptionReports => Set<CorruptionReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CivicDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}