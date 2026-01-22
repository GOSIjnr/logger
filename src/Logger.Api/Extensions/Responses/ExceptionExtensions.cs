using Logger.Api.Models;
using Logger.Application.Enums;
using Logger.Application.Models;

namespace Logger.Api.Extensions.Responses;

internal static class ExceptionExtensions
{
    private static readonly List<ResponseDetail> ContactDetail =
    [
        new ResponseDetail(
            Message: "We encountered an unexpected error. If the issue persists, contact support with trace id.",
            Severity: ResponseSeverity.Error
        )
    ];

    extension(Exception exception)
    {
        public ApiResponse<T> ToApiResponse<T>() => new(
            Success: false,
            MessageId: "UNEXPECTED_ERROR",
            Message: "Something went wrong on our side. Please try again later.",
            Details: ContactDetail,
            Data: default
        );
    }
}
