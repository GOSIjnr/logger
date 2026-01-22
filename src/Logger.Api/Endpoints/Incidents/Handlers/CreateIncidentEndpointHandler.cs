using Logger.Api.Extensions.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Incidents.CreateIncident;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Incidents.Handlers;

public static class CreateIncidentEndpointHandler
{
    public static async Task<IResult> Handle(
        [FromBody] CreateIncidentCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        OperationResult<Guid> result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/incidents/{result.Data}", result.ToApiResponse());
    }
}
