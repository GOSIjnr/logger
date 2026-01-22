using Logger.Application.CQRS.Messaging;
using Logger.Application.Constants.Responses;
using Logger.Application.Extensions.Responses;
using Logger.Application.Models;
using Logger.Application.Services;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Logger.Domain.Entities.UserSessions;
using Logger.Application.Features.Auth.Models;
using Logger.Application.Extensions.Entities;
using Logger.Application.Features.Auth.Extensions;

namespace Logger.Application.Features.Auth.LoginUser;

public class LoginUserCommandHandler(AppDbContext db, IHashingService hashingService, SessionManagementService sessionService) : IHandler<LoginUserCommand, OperationResult<SessionResult>>
{
    public async Task<OperationResult<SessionResult>> Handle(LoginUserCommand message, CancellationToken cancellationToken = default)
    {
        string emailHash = hashingService.HashEmail(message.Identifier);

        var userDto = await db.Users
            .AsNoTracking()
            .Where(u => u.EmailHash == emailHash || u.UserName == message.Identifier)
            .Select(u => new
            {
                u.Id,
                u.PasswordHash,
                u.IsLocked,
                u.Role,
            })
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw ResponseCatalog.Auth.InvalidCredentials.ToException();

        if (userDto.IsLocked)
            throw ResponseCatalog.Auth.InvalidCredentials.ToException();

        bool isPasswordValid = await hashingService.VerifyPasswordAsync(message.Password, userDto.PasswordHash);

        if (!isPasswordValid)
            throw ResponseCatalog.Auth.InvalidCredentials.ToException();

        UserSession sessionData = await sessionService.CreateSessionAsync(userDto.Id, userDto.Role, message.RememberMe, cancellationToken);

        SessionTimestampsResponse timeStamps = sessionData.ToTimestampsResponse();
        SessionResult data = new(sessionData.Id, timeStamps);

        return ResponseCatalog.Auth.LoginSuccessful
            .As<SessionResult>()
            .WithData(data)
            .ToOperationResult();
    }
}
