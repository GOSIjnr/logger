using Logger.Domain.Entities.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logger.Persistence.Configurations;

public class FieldValueConfiguration : IEntityTypeConfiguration<FieldValue>
{
    public void Configure(EntityTypeBuilder<FieldValue> builder)
    {
        builder.HasKey(x => new { x.IncidentId, x.FieldDefinitionId });

        builder.Property(x => x.Value)
            .HasMaxLength(4000);

        builder.OwnsOne(x => x.AuditState, audit =>
        {
            audit.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            audit.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsRequired();
        });

        builder.HasOne(x => x.FieldDefinition)
            .WithMany()
            .HasForeignKey(x => x.FieldDefinitionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
