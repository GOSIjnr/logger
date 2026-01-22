using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Enums;
using Logger.Application.Extensions.Entities;
using Logger.Application.Extensions.Responses;
using Logger.Application.Features.Users.Models;
using Logger.Application.Helpers;
using Logger.Application.Models;
using Logger.Application.Services;
using Logger.Domain.Entities.Users;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Logger.Application.Features.Users.GetCurrentUser;

public class GetCurrentUserQueryHandler(AppDbContext db, IDataEncryptionService encryptionService) : IHandler<GetCurrentUserQuery, OperationResult<UserResponse>>
{
    public async Task<OperationResult<UserResponse>> Handle(GetCurrentUserQuery message, CancellationToken cancellationToken = default)
    {
        if (message.UserId is null)
            throw ResponseCatalog.Auth.InvalidSession.ToException();

        User user = await db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == message.UserId.Value, cancellationToken)
            ?? throw ResponseCatalog.Auth.InvalidSession.ToException();

        if (user.IsLocked)
            throw ResponseCatalog.Auth.AccountLocked.ToException();

        UserSensitive sensitiveData = ObjectByteConverter.DeserializeFromBytes<UserSensitive>(
            encryptionService.Decrypt(user.EncryptedData, CryptoPurpose.UserSensitiveData)
        );

        user.SetSensitiveData(sensitiveData);

        return ResponseCatalog.User.Retrieved
            .As<UserResponse>()
            .WithData(user.ToUserResponse())
            .ToOperationResult();
    }
}
