using Logger.Application.Common.Responses;
using Logger.Application.Exceptions;

namespace Logger.Application.Extensions.Responses;

internal static class ResponseCatalogExtensions
{
    public static OperationOutcomeResponse<T> As<T>(this OperationOutcomeResponse response)
        => new(response.Id, response.Title, response.Details);

    public static AppException ToException(this OperationOutcomeResponse response)
        => new(new OperationFailureResponse(response.Id, 400, response.Title, response.Details));
}
