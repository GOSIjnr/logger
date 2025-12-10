using Logger.Api.Constants.Routes;
using Logger.Api.Endpoints.Users.GetUser;
using Logger.Api.Endpoints.Users.RegisterUser;

namespace Logger.Api.Endpoints.Users;

public static class UserEndpoints
{
    extension(IEndpointRouteBuilder routes)
    {
        public IEndpointRouteBuilder MapUserEndpoints()
        {
            RouteGroupBuilder group = routes
                .MapGroup(ApiRoutes.User.Base)
                .WithTags("Users");

            group.MapPost(ApiRoutes.User.CreateUser, RegisterUserHandler.Handle);
            group.MapGet(ApiRoutes.User.GetUserById, GetUserHandler.Handle);

            return routes;
        }
    }
}
