using Core.Abstractions.HandlerResults;

namespace Core.Abstractions.ServiceBus;

public interface IServiceBusResponse<out T> : IHandlerResult<T>
    where T : class
{
}
