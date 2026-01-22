using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Features.Sheets.Models;
using Logger.Application.Models;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Logger.Application.Features.Sheets.GetSheetsByOrganization;

public class GetSheetsByOrganizationHandler(AppDbContext db) : IHandler<GetSheetsByOrganizationQuery, OperationResult<IReadOnlyCollection<SheetResponse>>>
{
    public async Task<OperationResult<IReadOnlyCollection<SheetResponse>>> Handle(GetSheetsByOrganizationQuery message, CancellationToken cancellationToken = default)
    {
        var sheets = await db.Sheets
            .Where(x => x.OrganizationId == message.OrganizationId)
            .Select(x => new SheetResponse(x.Id, x.OrganizationId, x.Name, x.Description, x.CreatedAt, x.UpdatedAt))
            .ToListAsync(cancellationToken);

        return OperationResult<IReadOnlyCollection<SheetResponse>>.Success(sheets);
    }
}
