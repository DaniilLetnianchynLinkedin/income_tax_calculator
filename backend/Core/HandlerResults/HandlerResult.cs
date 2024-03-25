using Newtonsoft.Json;
using Core.Abstractions.Errors;
using Core.Abstractions.HandlerResults;

namespace Core.HandlerResults;

public abstract class HandlerResult<TPayload> : IHandlerResult<TPayload>
    where TPayload : class
{
    public HandlerResult()
        : this(string.Empty)
    {
        Success = false;
        Response = null;
    }

    public HandlerResult(string message)
    {
        Message = message;
        Response = null;
        Success = false;
    }

    public HandlerResult(IError error)
        : this(error?.Message ?? string.Empty)
    {
        AggregateError = error;
    }

    [JsonConstructor]
    public HandlerResult(TPayload response, string message)
    {
        Response = response;
        Message = message;
        Success = true;
    }

    public HandlerResult(TPayload response)
        : this(response, string.Empty)
    {
    }

    public HandlerResult(
        IError error,
        Dictionary<string, IEnumerable<IError>> propertiesValidationErrors)
        : this(error)
    {
        PropertiesValidationErrors = propertiesValidationErrors;
    }

    public bool Success { get; init; }

    public string Message { get; init; }

    public TPayload Response { get; init; }

    public IError AggregateError { get; init; }

    public Dictionary<string, IEnumerable<IError>> PropertiesValidationErrors { get; } = new();
}
