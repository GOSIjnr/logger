using Logger.Api.Constants.Routes;
using Logger.Api.Endpoints.Sheets.Handlers;
using Logger.Api.Models;
using Logger.Application.Features.Sheets.Models;

namespace Logger.Api.Endpoints.Sheets;

internal static class SheetEndpoints
{
    public static void MapSheetEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(ApiRoutes.Sheets.Base)
            .WithTags("Sheets")
            .RequireAuthorization();

        group.MapPost(ApiRoutes.Sheets.Create, CreateSheetEndpointHandler.Handle)
            .WithName(nameof(CreateSheetEndpointHandler))
            .Produces<ApiResponse<Guid>>(StatusCodes.Status201Created)
            .Produces<ApiResponse<object>>(StatusCodes.Status400BadRequest);

        group.MapGet(ApiRoutes.Sheets.ListByOrg, GetSheetsByOrganizationEndpointHandler.Handle)
            .WithName(nameof(GetSheetsByOrganizationEndpointHandler))
            .Produces<ApiResponse<IReadOnlyCollection<SheetResponse>>>(StatusCodes.Status200OK);
    }
}
