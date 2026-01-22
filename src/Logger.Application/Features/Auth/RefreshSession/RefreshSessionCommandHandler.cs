using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Features.Auth.Extensions;
using Logger.Application.Features.Auth.Models;
using Logger.Application.Models;
using Logger.Application.Services;

namespace Logger.Application.Features.Auth.RefreshSession;

public class RefreshSessionCommandHandler(SessionManagementService sessionService)
    : IHandler<RefreshSessionCommand, OperationResult<SessionResult>>
{
    public async Task<OperationResult<SessionResult>> Handle(RefreshSessionCommand message, CancellationToken cancellationToken = default)
    {
        if (message.SessionId is null)
            throw ResponseCatalog.Auth.InvalidSession.ToException();

        SessionData sessionData = await sessionService
            .ExtendSessionAsync(message.SessionId.Value, cancellationToken)
            ?? throw ResponseCatalog.Auth.InvalidSession.ToException();

        SessionTimestampsResponse timeStamps = sessionData.ToTimestampsResponse();
        SessionResult data = new(sessionData.SessionId, timeStamps);

        return ResponseCatalog.Auth.SessionRefreshed
            .As<SessionResult>()
            .WithData(data)
            .ToOperationResult();
    }
}
