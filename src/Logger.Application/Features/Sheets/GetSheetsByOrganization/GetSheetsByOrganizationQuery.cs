using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Sheets.Models;
using Logger.Application.Models;

namespace Logger.Application.Features.Sheets.GetSheetsByOrganization;

public record GetSheetsByOrganizationQuery(Guid OrganizationId) : IMessage<OperationResult<IReadOnlyCollection<SheetResponse>>>;
