using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Models;
using Logger.Application.Services;

namespace Logger.Application.Features.Auth.Logout;

public class LogoutUserCommandHandler(SessionManagementService sessionService) : IHandler<LogoutUserCommand, OperationResult<object>>
{
    public async Task<OperationResult<object>> Handle(LogoutUserCommand message, CancellationToken cancellationToken = default)
    {
        if (message.SessionId.HasValue)
            await sessionService.RevokeSessionAsync(message.SessionId.Value, cancellationToken);

        return ResponseCatalog.Auth.LogoutSuccessful
            .As<object>()
            .ToOperationResult();
    }
}
