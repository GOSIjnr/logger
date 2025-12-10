namespace Logger.Api.Endpoints.Users;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email
);
