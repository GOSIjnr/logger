using Logger.Api.Extensions.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Organizations.CreateOrganization;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Organizations.Handlers;

public static class CreateOrganizationEndpointHandler
{
    public static async Task<IResult> Handle(
        [FromBody] CreateOrganizationCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        OperationResult<Guid> result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/organizations/{result.Data}", result.ToApiResponse());
    }
}
