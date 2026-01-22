using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Users.Models;
using Logger.Application.Models;

namespace Logger.Application.Features.Users.GetUserById;

public record GetUserByIdQuery(
    Guid Id
) : IMessage<OperationResult<UserResponse>>;
