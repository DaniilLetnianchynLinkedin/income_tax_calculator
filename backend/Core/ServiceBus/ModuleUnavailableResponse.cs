using Core.Abstractions.Errors;
using Core.Abstractions.HandlerResults;

namespace Core.ServiceBus;

public class ModuleUnavailableResponse<T> : IHandlerResult<T>
    where T : class
{
    public bool Success { get; set; } = false;

    public T Response { get; set; } = default!;

    public IError AggregateError { get; set; } = null;

    public Dictionary<string, IEnumerable<IError>> PropertiesValidationErrors { get; set; } = new();

    public string Message { get; set; } = "Module is unavailable";
}
