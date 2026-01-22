using Logger.Domain.Entities.Incidents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logger.Persistence.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Severity)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(x => x.AuditState, audit =>
        {
            audit.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            audit.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsRequired();
        });

        builder.HasMany(x => x.FieldValues)
            .WithOne(x => x.Incident)
            .HasForeignKey(x => x.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
