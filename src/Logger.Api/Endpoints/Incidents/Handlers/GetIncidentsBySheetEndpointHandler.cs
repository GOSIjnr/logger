using Logger.Api.Extensions.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Incidents.GetIncidentsBySheet;
using Logger.Application.Features.Incidents.Models;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Incidents.Handlers;

public static class GetIncidentsBySheetEndpointHandler
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid sheetId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        OperationResult<IReadOnlyCollection<IncidentResponse>> result = await mediator.Send(new GetIncidentsBySheetQuery(sheetId), cancellationToken);
        return Results.Ok(result.ToApiResponse());
    }
}
