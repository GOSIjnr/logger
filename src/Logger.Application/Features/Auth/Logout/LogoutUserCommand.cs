using Logger.Application.CQRS.Messaging;
using Logger.Application.Models;

namespace Logger.Application.Features.Auth.Logout;

public record LogoutUserCommand(
    Guid? SessionId
) : IMessage<OperationResult<object>>;
