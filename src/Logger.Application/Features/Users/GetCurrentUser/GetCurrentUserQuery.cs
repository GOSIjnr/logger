using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Users.Models;
using Logger.Application.Models;

namespace Logger.Application.Features.Users.GetCurrentUser;

public record GetCurrentUserQuery(
    Guid? UserId
) : IMessage<OperationResult<UserResponse>>;
