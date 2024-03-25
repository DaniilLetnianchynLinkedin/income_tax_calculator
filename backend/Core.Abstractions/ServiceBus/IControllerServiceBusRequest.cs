using Core.Abstractions.DTO;

namespace Core.Abstractions.ServiceBus;

public interface IControllerServiceBusRequest<out TResponse> : IServiceBusRequest<TResponse>
    where TResponse : class, IOutgoingControllerDTO
{
}