using Logger.Domain.Enums;

namespace Logger.Application.Features.Incidents.Models;

public record IncidentResponse(
    Guid Id,
    Guid SheetId,
    string Title,
    IncidentStatus Status,
    IncidentSeverity Severity,
    DateTime? ResolvedAt,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    IReadOnlyCollection<FieldValueResponse> FieldValues
);

public record FieldValueResponse(
    Guid FieldDefinitionId,
    string Name,
    FieldType Type,
    string? Value
);
