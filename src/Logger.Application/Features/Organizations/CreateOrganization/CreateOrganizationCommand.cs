using Logger.Application.CQRS.Messaging;
using Logger.Application.Models;

namespace Logger.Application.Features.Organizations.CreateOrganization;

public record CreateOrganizationCommand(
    string Name,
    string? Description
) : IMessage<OperationResult<Guid>>;
