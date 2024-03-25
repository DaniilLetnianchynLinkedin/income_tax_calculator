using MediatR;
using Core.Abstractions.HandlerResults;

namespace Core.Abstractions.ServiceBus;

public interface IServiceBusRequest<out TResponse> : IRequest<IHandlerResult<TResponse>>, IRequest
    where TResponse : class
{
}
