using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Auth.Models;
using Logger.Application.Models;

namespace Logger.Application.Features.Auth.LoginUser;

public record LoginUserCommand(
    string Identifier,
    string Password,
    bool RememberMe = false
) : IMessage<OperationResult<SessionResult>>;
