namespace Core.Abstractions.ServiceBus;

public interface IServiceBusNotificationResponse
{
    public bool Success { get; set; }

    public string Message { get; set; }
}
