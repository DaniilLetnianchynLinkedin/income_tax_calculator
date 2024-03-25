using Core.Abstractions.Errors;

namespace Core.Abstractions.HandlerResults;

public interface IHandlerResult<out TResponse>
    where TResponse : class
{
    public bool Success { get; }

    public string Message { get; }

    public TResponse Response { get; }

    public IError AggregateError { get; }

    public Dictionary<string, IEnumerable<IError>> PropertiesValidationErrors { get; }
}
