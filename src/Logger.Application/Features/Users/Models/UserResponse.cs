using Logger.Domain.Enums;

namespace Logger.Application.Features.Users.Models;

public record UserResponse(
    Guid Id,
    string UserName,
    string FirstName,
    string? MiddleName,
    string LastName,
    SystemRole Role
);
