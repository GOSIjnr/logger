using Logger.Domain.Entities.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logger.Persistence.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Description)
            .HasMaxLength(1024);

        builder.OwnsOne(x => x.AuditState, audit =>
        {
            audit.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            audit.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsRequired();
        });

        builder.HasMany(x => x.Sheets)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
