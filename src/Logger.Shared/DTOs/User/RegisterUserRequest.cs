namespace Logger.Shared.DTOs.User;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password
);
