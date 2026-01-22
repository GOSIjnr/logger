using Logger.Application.Common.Responses;
using Logger.Application.Exceptions;

namespace Logger.Application.Extensions.Responses;

internal static class OperationFailureResponseExtensions
{
    extension(OperationFailureResponse response)
    {
        public AppException ToException() => new(response);
    }
}
