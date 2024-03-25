using Core.Abstractions.HandlerResults;

namespace Core.Abstractions.ServiceBus;

public interface IServiceBus
{
    Task<IHandlerResult<TResponse>> SendAsync<TRequest, TResponse>(TRequest payload, CancellationToken cancellationToken = default)
        where TRequest : IServiceBusRequest<TResponse>
        where TResponse : class;

    Task<IServiceBusNotificationResponse> Publish<TNotification>(
        TNotification payload,
        CancellationToken cancellationToken = default)
        where TNotification : IServiceBusNotification;
}