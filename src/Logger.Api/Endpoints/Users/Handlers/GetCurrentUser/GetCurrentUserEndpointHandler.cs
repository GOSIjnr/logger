using Logger.Api.Extensions.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Users.GetCurrentUser;
using Logger.Application.Features.Users.Models;
using Logger.Application.Models;
using System.Security.Claims;

namespace Logger.Api.Endpoints.Users.Handlers.GetCurrentUser;

internal static class GetCurrentUserEndpointHandler
{
    public static async Task<IResult> Handle(HttpContext context, IMediator mediator)
    {
        string? rawUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid? userId = Guid.TryParse(rawUserId, out Guid parsedUserId) ? parsedUserId : null;

        OperationResult<UserResponse> result = await mediator.Send(new GetCurrentUserQuery(userId));

        return Results.Ok(result.ToApiResponse());
    }
}
