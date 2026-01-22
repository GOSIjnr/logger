using Logger.Api.Constants.Routes;
using Logger.Api.Endpoints.Base.Handlers.GetInfo;

namespace Logger.Api.Endpoints.Base;

internal static class BaseEndpoints
{
    extension(IEndpointRouteBuilder app)
    {
        public void MapBaseEndpoints()
        {
            app.MapGet(ApiRoutes.ApiBasePath, GetInfoEndpointHandler.Handle)
                .ExcludeFromDescription();
        }
    }
}
