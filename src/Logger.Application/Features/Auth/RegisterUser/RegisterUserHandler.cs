using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Enums;
using Logger.Application.Extensions.Responses;
using Logger.Application.Helpers;
using Logger.Application.Models;
using Logger.Application.Services;
using Logger.Domain.Entities.Users;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Logger.Application.Features.Auth.RegisterUser;

public class RegisterUserCommandHandler(AppDbContext db, IHashingService hashingService, IDataEncryptionService encryptionService) : IHandler<RegisterUserCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(RegisterUserCommand message, CancellationToken cancellationToken = default)
    {
        string emailHash = hashingService.HashEmail(message.Email);

        if (await db.Users.AnyAsync(u => u.EmailHash == emailHash, cancellationToken))
            throw ResponseCatalog.Auth.EmailAlreadyExists.ToException();

        UserSensitive sensitiveData = UserSensitive.Create(
            firstName: message.FirstName,
            middleName: message.MiddleName,
            lastName: message.LastName,
            email: message.Email
        );

        byte[] sensitiveDataBytes = ObjectByteConverter.SerializeToBytes(sensitiveData);
        byte[] encryptedData = encryptionService.Encrypt(sensitiveDataBytes, CryptoPurpose.UserSensitiveData);

        string passwordHash = await hashingService.HashPasswordAsync(message.Password);

        User user = new(
            userName: message.UserName.Trim(),
            emailHash: emailHash,
            passwordHash: passwordHash
        );

        user.SetEncryptedData(encryptedData);

        db.Users.Add(user);
        await db.SaveChangesAsync(cancellationToken);

        return ResponseCatalog.Auth.RegistrationSuccessful
            .As<Guid>()
            .WithData(user.Id)
            .ToOperationResult();
    }
}
