namespace Logger.Application.Features.Sheets.Models;

public record SheetResponse(
    Guid Id,
    Guid OrganizationId,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
