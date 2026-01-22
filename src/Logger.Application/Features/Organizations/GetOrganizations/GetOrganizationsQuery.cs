using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Organizations.Models;
using Logger.Application.Models;

namespace Logger.Application.Features.Organizations.GetOrganizations;

public record GetOrganizationsQuery() : IMessage<OperationResult<IReadOnlyCollection<OrganizationResponse>>>;
