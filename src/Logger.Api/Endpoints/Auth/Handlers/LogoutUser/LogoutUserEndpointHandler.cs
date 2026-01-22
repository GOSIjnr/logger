using System.Security.Claims;
using Logger.Api.Constants.Auth;
using Logger.Api.Constants.Cookies;
using Logger.Api.Extensions.Responses;
using Logger.Api.Helpers;
using Logger.Api.Models;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Auth.Logout;
using Logger.Application.Models;

namespace Logger.Api.Endpoints.Auth.Handlers.LogoutUser;

internal static class LogoutUserEndpointHandler
{
    public static async Task<IResult> Handle(HttpContext context, HttpResponse response, IMediator mediator)
    {
        Guid? sessionId = null;
        string? rawSessionId = context.User.FindFirstValue(SessionClaimTypes.SessionId);

        if (Guid.TryParse(rawSessionId, out Guid parsedSessionId))
            sessionId = parsedSessionId;

        OperationResult<object> result = await mediator.Send(new LogoutUserCommand(sessionId));

        CookieHelper.DeleteCookie(response, CookieKeys.Session);

        ApiResponse<object> apiResponse = result.ToApiResponse();

        return Results.Ok(apiResponse);
    }
}
