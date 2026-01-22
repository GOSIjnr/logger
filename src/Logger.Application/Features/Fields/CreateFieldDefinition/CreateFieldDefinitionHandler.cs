using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Models;
using Logger.Domain.Entities.Fields;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Logger.Application.Features.Fields.CreateFieldDefinition;

public class CreateFieldDefinitionHandler(AppDbContext db) : IHandler<CreateFieldDefinitionCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(CreateFieldDefinitionCommand message, CancellationToken cancellationToken = default)
    {
        if (!await db.Sheets.AnyAsync(x => x.Id == message.SheetId, cancellationToken))
            throw ResponseCatalog.Sheet.NotFound.ToException();

        var definition = new FieldDefinition(message.SheetId, message.Name, message.Type, message.IsRequired);

        db.FieldDefinitions.Add(definition);
        await db.SaveChangesAsync(cancellationToken);

        return OperationResult<Guid>.Success(definition.Id);
    }
}
