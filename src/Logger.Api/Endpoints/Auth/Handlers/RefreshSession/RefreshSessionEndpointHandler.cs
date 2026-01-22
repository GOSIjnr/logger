using System.Security.Claims;
using Logger.Api.Constants.Auth;
using Logger.Api.Constants.Cookies;
using Logger.Api.Helpers;
using Logger.Api.Models;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Auth.Models;
using Logger.Application.Features.Auth.RefreshSession;
using Logger.Application.Models;

namespace Logger.Api.Endpoints.Auth.Handlers.RefreshSession;

internal static class RefreshSessionEndpointHandler
{
    public static async Task<IResult> Handle(HttpContext context, IMediator mediator, HttpResponse response)
    {
        Guid? sessionId = null;
        string? rawSessionId = context.User.FindFirstValue(SessionClaimTypes.SessionId);

        if (Guid.TryParse(rawSessionId, out Guid parsedSessionId))
            sessionId = parsedSessionId;

        OperationResult<SessionResult> result = await mediator.Send(new RefreshSessionCommand(sessionId));

        SessionResult data = result.Data!;

        CookieHelper.SetCookie(
            response,
            CookieKeys.Session,
            data.SessionId.ToString("N"),
            data.Timestamps.ExpiresAt
        );

        ApiResponse<SessionTimestampsResponse> apiResponse = new(
            Success: true,
            MessageId: result.MessageId,
            Message: result.Message,
            Details: result.Details,
            Data: data.Timestamps
        );

        return Results.Ok(apiResponse);
    }
}
