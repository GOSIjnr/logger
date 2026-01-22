using System.Security.Claims;
using Logger.Application.Models;
using Logger.Api.Constants.Auth;

namespace Logger.Api.Extensions.Claims;

internal static class SessionDataExtensions
{
    extension(SessionData session)
    {
        public IEnumerable<Claim> ToClaims()
        {
            yield return new Claim(ClaimTypes.NameIdentifier, session.UserId.ToString("N"));
            yield return new Claim(ClaimTypes.Role, session.Role.ToString());
            yield return new Claim(SessionClaimTypes.SessionId, session.SessionId.ToString("N"));
        }
    }
}
