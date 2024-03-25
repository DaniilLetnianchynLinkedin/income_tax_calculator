using Core.Abstractions.ServiceBus;

namespace Core.ServiceBus;

public class ServiceBusNotificationResponse : IServiceBusNotificationResponse
{
    public bool Success { get; set; }

    public string Message { get; set; }
}
