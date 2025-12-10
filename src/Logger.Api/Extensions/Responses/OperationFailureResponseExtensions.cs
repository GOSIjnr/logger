using Logger.Api.Common.Responses;
using Logger.Api.Exceptions;

namespace Logger.Api.Extensions.Responses;

public static class OperationFailureResponseExtensions
{
    extension(OperationFailureResponse response)
    {
        public AppException ToException() => new(response);
    }
}
