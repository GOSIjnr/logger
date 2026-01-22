using Logger.Domain.Entities.Sheets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logger.Persistence.Configurations;

public class SheetConfiguration : IEntityTypeConfiguration<Sheet>
{
    public void Configure(EntityTypeBuilder<Sheet> builder)
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

        builder.HasMany(x => x.Incidents)
            .WithOne(x => x.Sheet)
            .HasForeignKey(x => x.SheetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.FieldDefinitions)
            .WithOne(x => x.Sheet)
            .HasForeignKey(x => x.SheetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
