using Logger.Api.Constants.Routes;
using Logger.Api.Extensions.Responses;
using Logger.Api.Models;
using Logger.Application.CQRS.Messaging;
using Logger.Application.Features.Auth.RegisterUser;
using Logger.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logger.Api.Endpoints.Auth.Handlers.RegisterUser;

internal static class RegisterUserEndpointHandler
{
    public static async Task<IResult> Handle([FromBody] RegisterUserCommand request, IMediator mediator)
    {
        OperationResult<Guid> result = await mediator.Send(request);

        Guid userId = result.Data;
        string location = $"{ApiRoutes.User.Base}/{userId}";

        ApiResponse<object> response = result.WithoutData()
            .ToApiResponse();

        return Results.Created(location, response);
    }
}
