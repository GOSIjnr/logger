using Logger.Domain.Abstractions;
using Logger.Domain.Components.Auditing;
using Logger.Domain.Entities.Sheets;
using Logger.Domain.Enums;

namespace Logger.Domain.Entities.Fields;

public class FieldDefinition : IEntity, IAuditable
{
    public readonly AuditState AuditState = new();

    private FieldDefinition() { }

    public FieldDefinition(Guid sheetId, string name, FieldType type, bool isRequired = false)
    {
        SheetId = sheetId;
        SetName(name);
        SetType(type);
        IsRequired = isRequired;
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public DateTime CreatedAt => AuditState.CreatedAt;
    public DateTime UpdatedAt => AuditState.UpdatedAt;

    public Guid SheetId { get; private set; }
    public Sheet Sheet { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;
    public FieldType Type { get; private set; }
    public bool IsRequired { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        Name = name;
        AuditState.UpdateAudit();
    }

    public void SetType(FieldType type)
    {
        Type = type;
        AuditState.UpdateAudit();
    }

    public void SetRequired(bool isRequired)
    {
        IsRequired = isRequired;
        AuditState.UpdateAudit();
    }
}
