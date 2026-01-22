using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Models;
using Logger.Domain.Entities.Organizations;
using Logger.Persistence.Context;

namespace Logger.Application.Features.Organizations.CreateOrganization;

public class CreateOrganizationHandler(AppDbContext db) : IHandler<CreateOrganizationCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(CreateOrganizationCommand message, CancellationToken cancellationToken = default)
    {
        var organization = new Organization(message.Name, message.Description);

        db.Organizations.Add(organization);
        await db.SaveChangesAsync(cancellationToken);

        return ResponseCatalog.Organization.Created
            .As<Guid>()
            .WithData(organization.Id)
            .ToOperationResult();
    }
}
