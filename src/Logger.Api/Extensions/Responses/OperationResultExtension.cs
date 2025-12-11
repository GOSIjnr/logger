using Logger.Api.Models;
using Logger.Shared.Models;

namespace Logger.Api.Extensions.Responses;

public static class OperationResultExtension
{
    public static ApiResponse<T> ToApiResponse<T>(this OperationResult<T> result)
    {
        return new ApiResponse<T>(null, true, result.MessageId, result.Message, result.Details, result.Data);
    }
}
