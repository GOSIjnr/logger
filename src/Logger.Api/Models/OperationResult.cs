using Logger.Shared.Models;

namespace Logger.Api.Models;

public record OperationResult<T>(
	string MessageId,
	string Message,
	List<ResponseDetail>? Details,
	T? Data
);
