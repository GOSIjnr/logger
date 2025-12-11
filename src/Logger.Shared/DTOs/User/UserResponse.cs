namespace Logger.Shared.DTOs.User;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email
);
