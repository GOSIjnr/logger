using Logger.Application.Features.Auth.Models;
using Logger.Application.Models;

namespace Logger.Application.Features.Auth.Extensions;

internal static class SessionDataExtensions
{
    extension(SessionData sessionData)
    {
        public SessionTimestampsResponse ToTimestampsResponse() => new(
            sessionData.ExpiresAt,
            sessionData.AbsoluteExpiresAt
        );
    }
}
