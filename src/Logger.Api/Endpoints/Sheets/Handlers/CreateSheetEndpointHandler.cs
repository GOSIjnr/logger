using Logger.Api.Extensions.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Sheets.CreateSheet;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Sheets.Handlers;

public static class CreateSheetEndpointHandler
{
    public static async Task<IResult> Handle(
        [FromBody] CreateSheetCommand command,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        OperationResult<Guid> result = await mediator.Send(command, cancellationToken);
        return Results.Created($"/api/sheets/{result.Data}", result.ToApiResponse());
    }
}
