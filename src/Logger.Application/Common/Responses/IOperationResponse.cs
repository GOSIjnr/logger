using Logger.Application.Models;

namespace Logger.Application.Common.Responses;

internal interface IOperationResponse
{
    public string Id { get; }
    public string Title { get; }
    public ResponseDetail[] Details { get; }
}
