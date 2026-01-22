using Logger.Api.Extensions.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Users.GetUserById;
using Logger.Application.Features.Users.Models;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Users.Handlers.GetUserById;

internal static class GetUserByIdEndpointHandler
{
    public static async Task<IResult> Handle([FromRoute] Guid id, IMediator mediator)
    {
        OperationResult<UserResponse> result = await mediator.Send(new GetUserByIdQuery(id));

        return Results.Ok(result.ToApiResponse());
    }
}
