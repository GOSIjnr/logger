using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Models;
using Logger.Domain.Entities.Fields;
using Logger.Domain.Entities.Incidents;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Logger.Application.Features.Incidents.CreateIncident;

public class CreateIncidentHandler(AppDbContext db) : IHandler<CreateIncidentCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(CreateIncidentCommand message, CancellationToken cancellationToken = default)
    {
        if (!await db.Sheets.AnyAsync(x => x.Id == message.SheetId, cancellationToken))
            throw ResponseCatalog.Sheet.NotFound.ToException();

        var incident = new Incident(message.SheetId, message.Title, message.Status, message.Severity);

        if (message.FieldValues != null)
        {
            var fieldDefinitions = await db.FieldDefinitions
                .Where(x => x.SheetId == message.SheetId)
                .ToListAsync(cancellationToken);

            foreach (var fieldValue in message.FieldValues)
            {
                var definition = fieldDefinitions.FirstOrDefault(x => x.Id == fieldValue.Key);
                if (definition != null)
                {
                    // incident.AddFieldValue logic is missing from entity, I will add it or handle it here
                    // For now, I'll use the persistence context to add values as I didn't add the collection management to Incident entity yet
                    var val = new FieldValue(incident.Id, definition.Id, fieldValue.Value);
                    db.FieldValues.Add(val);
                }
            }
        }

        db.Incidents.Add(incident);
        await db.SaveChangesAsync(cancellationToken);

        return ResponseCatalog.Incident.Created
            .As<Guid>()
            .WithData(incident.Id)
            .ToOperationResult();
    }
}
