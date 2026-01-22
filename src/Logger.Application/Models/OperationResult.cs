namespace Logger.Application.Models;

public partial record OperationResult<T>(
    string MessageId,
    string Message,
    List<ResponseDetail>? Details,
    T? Data
);
