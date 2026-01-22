using System.Security.Claims;
using System.Text.Encodings.Web;
using Logger.Api.Constants.Cookies;
using Logger.Api.Extensions.Claims;
using Logger.Api.Helpers;
using Logger.Application.Models;
using Logger.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Logger.Api.Authentication;

internal class SessionAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, SessionManagementService sessionService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Endpoint? endpoint = Context.GetEndpoint();

        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
            return AuthenticateResult.NoResult();

        if (!TryGetSessionIdFromCookie(out Guid sessionId))
            return AuthenticateResult.NoResult();

        SessionData? sessionData = await sessionService.GetSessionDataAsync(sessionId, Context.RequestAborted);

        if (sessionData is null || sessionData.IsLocked || sessionData.IsExpired())
            return AuthenticateResult.Fail("Invalid or expired session");

        IEnumerable<Claim> claims = sessionData.ToClaims();

        ClaimsIdentity identity = new(claims, Scheme.Name);
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    private bool TryGetSessionIdFromCookie(out Guid sessionId)
    {
        string? rawSessionId = CookieHelper.GetCookie(Context.Request, CookieKeys.Session);
        return Guid.TryParse(rawSessionId, out sessionId);
    }
}
