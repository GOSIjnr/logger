using Logger.Api.Endpoints.Users;
using Logger.Api.Entities;

namespace Logger.Api.Extensions.Entities;

public static class UserExtenstions
{
    extension(User user)
    {
        public UserResponse ToUserResponse()
        {
            if (user.SensitiveData is null)
                throw new InvalidOperationException("User does not have sensitive data.");

            UserResponse response = new(
                Id: user.Id,
                FirstName: user.SensitiveData.FirstName,
                LastName: user.SensitiveData.LastName,
                UserName: user.UserName,
                Email: user.SensitiveData.Email
            );

            return response;
        }
    }
}