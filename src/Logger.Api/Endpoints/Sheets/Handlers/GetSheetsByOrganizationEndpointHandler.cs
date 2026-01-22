using Logger.Api.Extensions.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Sheets.GetSheetsByOrganization;
using Logger.Application.Features.Sheets.Models;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Sheets.Handlers;

public static class GetSheetsByOrganizationEndpointHandler
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid orgId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        OperationResult<IReadOnlyCollection<SheetResponse>> result = await mediator.Send(new GetSheetsByOrganizationQuery(orgId), cancellationToken);
        return Results.Ok(result.ToApiResponse());
    }
}
