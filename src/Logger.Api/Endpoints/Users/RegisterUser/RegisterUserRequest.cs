namespace Logger.Api.Endpoints.Users.RegisterUser;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password
);
