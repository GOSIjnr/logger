using Logger.Application.Models;

namespace Logger.Api.Models;

internal record ApiResponse<T>(
    bool Success,
    string MessageId,
    string Message,
    List<ResponseDetail>? Details,
    T? Data
);
