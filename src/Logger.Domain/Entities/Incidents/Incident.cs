using Logger.Domain.Abstractions;
using Logger.Domain.Components.Auditing;
using Logger.Domain.Entities.Fields;
using Logger.Domain.Entities.Sheets;
using Logger.Domain.Enums;

namespace Logger.Domain.Entities.Incidents;

public class Incident : IEntity, IAuditable
{
    public readonly AuditState AuditState = new();

    private Incident() { }

    public Incident(Guid sheetId, string title, IncidentStatus status = IncidentStatus.Open, IncidentSeverity severity = IncidentSeverity.Medium)
    {
        SheetId = sheetId;
        SetTitle(title);
        SetStatus(status);
        SetSeverity(severity);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();

    public DateTime CreatedAt => AuditState.CreatedAt;
    public DateTime UpdatedAt => AuditState.UpdatedAt;

    public Guid SheetId { get; private set; }
    public Sheet Sheet { get; private set; } = null!;

    public string Title { get; private set; } = string.Empty;
    public IncidentStatus Status { get; private set; }
    public IncidentSeverity Severity { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    public IReadOnlyCollection<FieldValue> FieldValues { get; private set; } = [];

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));

        Title = title;
        AuditState.UpdateAudit();
    }

    public void SetStatus(IncidentStatus status)
    {
        Status = status;
        if (status == IncidentStatus.Closed && ResolvedAt == null)
        {
            ResolvedAt = DateTime.UtcNow;
        }
        else if (status != IncidentStatus.Closed)
        {
            ResolvedAt = null;
        }
        AuditState.UpdateAudit();
    }

    public void SetSeverity(IncidentSeverity severity)
    {
        Severity = severity;
        AuditState.UpdateAudit();
    }
}
