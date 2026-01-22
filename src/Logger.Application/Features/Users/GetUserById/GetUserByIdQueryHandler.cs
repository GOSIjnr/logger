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

namespace Logger.Application.Features.Users.GetUserById;

public class GetUserByIdQueryHandler(AppDbContext db, IDataEncryptionService encryptionService) : IHandler<GetUserByIdQuery, OperationResult<UserResponse>>
{
    public async Task<OperationResult<UserResponse>> Handle(GetUserByIdQuery message, CancellationToken cancellationToken = default)
    {
        User? user = await db.Users
            .FirstOrDefaultAsync(u => u.Id == message.Id, cancellationToken)
            ?? throw ResponseCatalog.User.NotFound.ToException();

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
