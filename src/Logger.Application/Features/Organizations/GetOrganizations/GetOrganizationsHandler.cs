using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Features.Organizations.Models;
using Logger.Application.Models;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Logger.Application.Features.Organizations.GetOrganizations;

public class GetOrganizationsHandler(AppDbContext db) : IHandler<GetOrganizationsQuery, OperationResult<IReadOnlyCollection<OrganizationResponse>>>
{
    public async Task<OperationResult<IReadOnlyCollection<OrganizationResponse>>> Handle(GetOrganizationsQuery message, CancellationToken cancellationToken = default)
    {
        var organizations = await db.Organizations
            .Select(x => new OrganizationResponse(x.Id, x.Name, x.Description, x.CreatedAt, x.UpdatedAt))
            .ToListAsync(cancellationToken);

        return OperationResult<IReadOnlyCollection<OrganizationResponse>>.Success(organizations);
    }
}
