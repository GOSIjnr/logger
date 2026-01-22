using Logger.Application.CQRS.Messaging;

namespace Logger.Application.CQRS.Decorators;

public interface IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage<TResponse>
{
    Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken = default);
}
