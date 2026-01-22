using Logger.Api.Extensions.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Organizations.GetOrganizations;
using Logger.Application.Features.Organizations.Models;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Organizations.Handlers;

public static class GetOrganizationsEndpointHandler
{
    public static async Task<IResult> Handle(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        OperationResult<IReadOnlyCollection<OrganizationResponse>> result = await mediator.Send(new GetOrganizationsQuery(), cancellationToken);
        return Results.Ok(result.ToApiResponse());
    }
}
