using Logger.Api.Constants.Routes;
using Logger.Api.Endpoints.Organizations.Handlers;
using Logger.Api.Models;
using Logger.Application.Features.Organizations.Models;

namespace Logger.Api.Endpoints.Organizations;

internal static class OrganizationEndpoints
{
    public static void MapOrganizationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(ApiRoutes.Organizations.Base)
            .WithTags("Organizations")
            .RequireAuthorization();

        group.MapPost(ApiRoutes.Organizations.Create, CreateOrganizationEndpointHandler.Handle)
            .WithName(nameof(CreateOrganizationEndpointHandler))
            .Produces<ApiResponse<Guid>>(StatusCodes.Status201Created)
            .Produces<ApiResponse<object>>(StatusCodes.Status400BadRequest);

        group.MapGet(ApiRoutes.Organizations.List, GetOrganizationsEndpointHandler.Handle)
            .WithName(nameof(GetOrganizationsEndpointHandler))
            .Produces<ApiResponse<IReadOnlyCollection<OrganizationResponse>>>(StatusCodes.Status200OK);
    }
}
