using System.Text.Json;
using Logger.Api.Endpoints.Users.RegisterUser;
using Logger.Api.Entities;
using Logger.Api.Interfaces.Services;
using Logger.Api.Models;

namespace Logger.Api.Endpoints.Users;

public class UserFactory
{
    public static User Create(RegisterUserRequest request, string normalizedEmail, string emailHash, string passwordHash, IDataEncryptionService dataEncryptionService)
    {
        User user = new(request.UserName.Trim(), emailHash, passwordHash);

        UserSensitive sensitiveData = new()
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = normalizedEmail
        };

        byte[] dataBlob = JsonSerializer.SerializeToUtf8Bytes(sensitiveData);
        byte[] encryptedData = dataEncryptionService.EncryptData(dataBlob);

        user.SetSensitiveData(sensitiveData);
        user.SetEncryptedData(encryptedData);

        return user;
    }
}
