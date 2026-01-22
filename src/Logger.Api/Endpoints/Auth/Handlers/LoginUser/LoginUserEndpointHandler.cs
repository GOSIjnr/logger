using Logger.Api.Constants.Cookies;
using Logger.Api.Helpers;
using Logger.Api.Models;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Auth.LoginUser;
using Logger.Application.Features.Auth.Models;
using Logger.Application.Features.Auth.RevokeSession;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Auth.Handlers.LoginUser;

internal static class LoginUserEndpointHandler
{
    public static async Task<IResult> Handle([FromBody] LoginUserCommand message, IMediator mediator, HttpResponse response, HttpRequest request)
    {
        OperationResult<SessionResult> result = await mediator.Send(message);

        string? rawSessionId = CookieHelper.GetCookie(request, CookieKeys.Session);

        if (Guid.TryParse(rawSessionId, out Guid sessionId))
            await mediator.Send(new RevokeSessionCommand(sessionId));

        SessionResult data = result.Data!;

        CookieHelper.SetCookie(
            response,
            CookieKeys.Session,
            data.SessionId.ToString("N"),
            expiresUtc: data.Timestamps.ExpiresAt
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
