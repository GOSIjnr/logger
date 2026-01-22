using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Models;
using Logger.Application.Services;

namespace Logger.Application.Features.Auth.RevokeSession;

public class RevokeSessionCommandHandler(SessionManagementService sessionService) : IHandler<RevokeSessionCommand, OperationResult<object>>
{
    public async Task<OperationResult<object>> Handle(RevokeSessionCommand message, CancellationToken cancellationToken = default)
    {
        if (message.SessionId.HasValue)
            await sessionService.RevokeSessionAsync(message.SessionId.Value, cancellationToken);

        return ResponseCatalog.Auth.SessionRevoked
            .As<object>()
            .ToOperationResult();
    }
}
