using Logger.Application.CQRS.Messaging;
using Logger.Application.Models;
using Logger.Domain.Enums;

namespace Logger.Application.Features.Incidents.CreateIncident;

public record CreateIncidentCommand(
    Guid SheetId,
    string Title,
    IncidentStatus Status = IncidentStatus.Open,
    IncidentSeverity Severity = IncidentSeverity.Medium,
    Dictionary<Guid, string?>? FieldValues = null
) : IMessage<OperationResult<Guid>>;
