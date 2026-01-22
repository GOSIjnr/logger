using Logger.Application.Constants.Responses;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Extensions.Responses;
using Logger.Application.Models;
using Logger.Domain.Entities.Sheets;
using Logger.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Logger.Application.Features.Sheets.CreateSheet;

public class CreateSheetHandler(AppDbContext db) : IHandler<CreateSheetCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(CreateSheetCommand message, CancellationToken cancellationToken = default)
    {
        if (!await db.Organizations.AnyAsync(x => x.Id == message.OrganizationId, cancellationToken))
            throw ResponseCatalog.Organization.NotFound.ToException();

        var sheet = new Sheet(message.OrganizationId, message.Name, message.Description);

        db.Sheets.Add(sheet);
        await db.SaveChangesAsync(cancellationToken);

        return ResponseCatalog.Sheet.Created
            .As<Guid>()
            .WithData(sheet.Id)
            .ToOperationResult();
    }
}
