using Logger.Api.Constants.Routes;
using Logger.Api.Endpoints.Incidents.Handlers;
using Logger.Api.Models;
using Logger.Application.Features.Incidents.Models;

namespace Logger.Api.Endpoints.Incidents;

internal static class IncidentEndpoints
{
    public static void MapIncidentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(ApiRoutes.Incidents.Base)
            .WithTags("Incidents")
            .RequireAuthorization();

        group.MapPost(ApiRoutes.Incidents.Create, CreateIncidentEndpointHandler.Handle)
            .WithName(nameof(CreateIncidentEndpointHandler))
            .Produces<ApiResponse<Guid>>(StatusCodes.Status201Created)
            .Produces<ApiResponse<object>>(StatusCodes.Status400BadRequest);

        group.MapGet(ApiRoutes.Incidents.ListBySheet, GetIncidentsBySheetEndpointHandler.Handle)
            .WithName(nameof(GetIncidentsBySheetEndpointHandler))
            .Produces<ApiResponse<IReadOnlyCollection<IncidentResponse>>>(StatusCodes.Status200OK);
    }
}
