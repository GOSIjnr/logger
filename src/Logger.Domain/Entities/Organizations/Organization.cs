using Logger.Domain.Abstractions;
using Logger.Domain.Components.Auditing;
using Logger.Domain.Entities.Sheets;

namespace Logger.Domain.Entities.Organizations;

public class Organization : IEntity, IAuditable
{
    public readonly AuditState AuditState = new();

    private Organization() { }

    public Organization(string name, string? description = null)
    {
        SetName(name);
        SetDescription(description);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public DateTime CreatedAt => AuditState.CreatedAt;
    public DateTime UpdatedAt => AuditState.UpdatedAt;

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public IReadOnlyCollection<Sheet> Sheets { get; private set; } = [];

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
