using Logger.Api.Models;

namespace Logger.Api.Common.Responses;

public record OperationFailureResponse(
	string Id,
	int StatusCode,
	string Title,
	ResponseDetail[] Details
) : BaseOperationResponse<OperationFailureResponse>(Id, Title, Details);
