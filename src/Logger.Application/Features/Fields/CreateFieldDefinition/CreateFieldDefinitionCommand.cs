using Logger.Application.CQRS.Messaging;
using Logger.Application.Models;
using Logger.Domain.Enums;

namespace Logger.Application.Features.Fields.CreateFieldDefinition;

public record CreateFieldDefinitionCommand(
    Guid SheetId,
    string Name,
    FieldType Type,
    bool IsRequired = false
) : IMessage<OperationResult<Guid>>;
