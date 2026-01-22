using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Features.Incidents.Models;
using Logger.Application.Models;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Logger.Application.Features.Incidents.GetIncidentsBySheet;

public class GetIncidentsBySheetHandler(AppDbContext db) : IHandler<GetIncidentsBySheetQuery, OperationResult<IReadOnlyCollection<IncidentResponse>>>
{
    public async Task<OperationResult<IReadOnlyCollection<IncidentResponse>>> Handle(GetIncidentsBySheetQuery message, CancellationToken cancellationToken = default)
    {
        var incidents = await db.Incidents
            .Where(x => x.SheetId == message.SheetId)
            .Include(x => x.FieldValues)
                .ThenInclude(x => x.FieldDefinition)
            .Select(x => new IncidentResponse(
                x.Id,
                x.SheetId,
                x.Title,
                x.Status,
                x.Severity,
                x.ResolvedAt,
                x.CreatedAt,
                x.UpdatedAt,
                x.FieldValues.Select(v => new FieldValueResponse(
                    v.FieldDefinitionId,
                    v.FieldDefinition.Name,
                    v.FieldDefinition.Type,
                    v.Value
                )).ToList()
            ))
            .ToListAsync(cancellationToken);

        return OperationResult<IReadOnlyCollection<IncidentResponse>>.Success(incidents);
    }
}
