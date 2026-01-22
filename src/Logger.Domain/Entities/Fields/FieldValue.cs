using Logger.Domain.Abstractions;
using Logger.Domain.Components.Auditing;
using Logger.Domain.Entities.Incidents;

namespace Logger.Domain.Entities.Fields;

public class FieldValue : IAuditable
{
    public readonly AuditState AuditState = new();

    private FieldValue() { }

    public FieldValue(Guid incidentId, Guid fieldDefinitionId, string? value)
    {
        IncidentId = incidentId;
        FieldDefinitionId = fieldDefinitionId;
        SetValue(value);
    }

    public DateTime CreatedAt => AuditState.CreatedAt;
    public DateTime UpdatedAt => AuditState.UpdatedAt;

    public Guid IncidentId { get; private set; }
    public Incident Incident { get; private set; } = null!;

    public Guid FieldDefinitionId { get; private set; }
    public FieldDefinition FieldDefinition { get; private set; } = null!;

    public string? Value { get; private set; }

    public void SetValue(string? value)
    {
        Value = value;
        AuditState.UpdateAudit();
    }
}
