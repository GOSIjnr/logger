using Logger.Domain.Abstractions;
using Logger.Domain.Components.Auditing;
using Logger.Domain.Entities.Fields;
using Logger.Domain.Entities.Incidents;
using Logger.Domain.Entities.Organizations;

namespace Logger.Domain.Entities.Sheets;

public class Sheet : IEntity, IAuditable
{
    public readonly AuditState AuditState = new();

    private Sheet() { }

    public Sheet(Guid organizationId, string name, string? description = null)
    {
        OrganizationId = organizationId;
        SetName(name);
        SetDescription(description);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public DateTime CreatedAt => AuditState.CreatedAt;
    public DateTime UpdatedAt => AuditState.UpdatedAt;

    public Guid OrganizationId { get; private set; }
    public Organization Organization { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public IReadOnlyCollection<Incident> Incidents { get; private set; } = [];
    public IReadOnlyCollection<FieldDefinition> FieldDefinitions { get; private set; } = [];

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Name = name;
        AuditState.UpdateAudit();
    }

    public void SetDescription(string? description)
    {
        Description = description;
        AuditState.UpdateAudit();
    }
}
