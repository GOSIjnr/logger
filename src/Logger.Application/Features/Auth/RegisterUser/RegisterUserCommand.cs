using Logger.Application.CQRS.Messaging;
using Logger.Application.Models;

namespace Logger.Application.Features.Auth.RegisterUser;

public record RegisterUserCommand(
    string FirstName,
    string? MiddleName,
    string LastName,
    string UserName,
    string Email,
    string Password
) : IMessage<OperationResult<Guid>>;
