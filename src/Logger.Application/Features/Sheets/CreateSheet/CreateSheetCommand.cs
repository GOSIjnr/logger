using Logger.Application.CQRS.Messaging;
using Logger.Application.Models;

namespace Logger.Application.Features.Sheets.CreateSheet;

public record CreateSheetCommand(
    Guid OrganizationId,
    string Name,
    string? Description
) : IMessage<OperationResult<Guid>>;
