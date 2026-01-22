using Logger.Application.Features.Auth.Models;
using Logger.Application.Models;
using Logger.Domain.Entities.UserSessions;
using Logger.Domain.Enums;

namespace Logger.Application.Extensions.Entities;

internal static class UserSessionExtensions
{
    extension(UserSession session)
    {
        public SessionData ToSessionData(SystemRole role, bool isLocked) => new(
            SessionId: session.Id,
            UserId: session.UserId,
            ExpiresAt: session.ExpiresAt,
            AbsoluteExpiresAt: session.AbsoluteExpiresAt,
            IsRevoked: session.IsRevoked,
            RememberMe: session.RememberMe,
            IsLocked: isLocked,
            Role: role
        );

        public SessionTimestampsResponse ToTimestampsResponse() => new(
            session.ExpiresAt,
            session.AbsoluteExpiresAt
        );
    }
}
