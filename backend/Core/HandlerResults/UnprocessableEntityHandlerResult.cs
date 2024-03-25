using Newtonsoft.Json;
using Core.Abstractions.Errors;
using Core.Abstractions.HandlerResults;

namespace Core.HandlerResults;

public class UnprocessableEntityHandlerResult<TPayload> : HandlerResult<TPayload>, IUnprocessableEntityHandlerResult<TPayload>
    where TPayload : class
{
    public UnprocessableEntityHandlerResult()
    {
    }

    [JsonConstructor]
    public UnprocessableEntityHandlerResult(IError error, Dictionary<string, IEnumerable<IError>> propertiesValidationErrors)
        : base(error, propertiesValidationErrors)
    {
    }
}
