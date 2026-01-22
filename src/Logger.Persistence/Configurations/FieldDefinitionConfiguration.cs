using Logger.Domain.Entities.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logger.Persistence.Configurations;

public class FieldDefinitionConfiguration : IEntityTypeConfiguration<FieldDefinition>
{
    public void Configure(EntityTypeBuilder<FieldDefinition> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(x => x.AuditState, audit =>
        {
            audit.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            audit.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsRequired();
        });
    }
}
