using Core.HandlerResults;

namespace Core.ServiceBus;

public class ServiceBusResponse<T> : HandlerResult<T>
    where T : class
{
    public ServiceBusResponse(string message)
        : base(message)
    {
    }
}
