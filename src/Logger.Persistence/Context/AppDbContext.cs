using Logger.Domain.Entities.Fields;
using Logger.Domain.Entities.Incidents;
using Logger.Domain.Entities.Organizations;
using Logger.Domain.Entities.Sheets;
using Logger.Domain.Entities.Users;
using Logger.Domain.Entities.UserSessions;
using Microsoft.EntityFrameworkCore;

namespace Logger.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Sheet> Sheets => Set<Sheet>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<FieldDefinition> FieldDefinitions => Set<FieldDefinition>();
    public DbSet<FieldValue> FieldValues => Set<FieldValue>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
