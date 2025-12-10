using Logger.Api.Models;

namespace Logger.Api.Common.Responses;

public interface IOperationResponse
{
    public string Id { get; }
    public string Title { get; }
    public ResponseDetail[] Details { get; }
}
