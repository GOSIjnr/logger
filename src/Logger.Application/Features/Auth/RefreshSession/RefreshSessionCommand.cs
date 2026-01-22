using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Auth.Models;
using Logger.Application.Models;

namespace Logger.Application.Features.Auth.RefreshSession;

public record RefreshSessionCommand(
    Guid? SessionId
) : IMessage<OperationResult<SessionResult>>;
