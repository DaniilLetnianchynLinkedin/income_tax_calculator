using Core.Abstractions.ServiceBus;

namespace Core.ServiceBus;

public class ListenersModulesAreUnavailableNotificationResponse : IServiceBusNotificationResponse
{
    public bool Success { get; set; } = false;

    public string Message { get; set; } = "Not a single listener of the notification was found. Required modules are unavailable";
}
