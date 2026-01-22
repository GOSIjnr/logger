using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Incidents.Models;
using Logger.Application.Models;

namespace Logger.Application.Features.Incidents.GetIncidentsBySheet;

public record GetIncidentsBySheetQuery(Guid SheetId) : IMessage<OperationResult<IReadOnlyCollection<IncidentResponse>>>;
