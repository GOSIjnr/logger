using Logger.Application.CQRS.Messaging;
using Logger.Application.Models;

namespace Logger.Application.Features.Auth.RevokeSession;

public record RevokeSessionCommand(
    Guid? SessionId
) : IMessage<OperationResult<object>>;
