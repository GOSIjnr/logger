using Logger.Domain.Enums;

namespace Logger.Api.Constants.Auth;

internal static class AuthorizationRoles
{
    public const string User = nameof(SystemRole.User);
    public const string Admin = nameof(SystemRole.Admin);
    public const string SuperAdmin = nameof(SystemRole.SuperAdmin);
}
