namespace Logger.Application.Features.Organizations.Models;

public record OrganizationResponse(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
