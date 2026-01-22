using Logger.Domain.Enums;

namespace Logger.Api.Models;

internal record InternalSessionData(
    Guid UserId,
    SystemRole Role
);
