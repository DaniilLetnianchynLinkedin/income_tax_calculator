using Core.Abstractions.Errors;
using Core.Abstractions.HandlerResults;

namespace Core.HandlerResults;

public class BadRequestHandlerResult<TPayload> : HandlerResult<TPayload>, IBadRequestHandlerResult<TPayload>
    where TPayload : class
{
    public BadRequestHandlerResult(IError error)
        : base(error)
    {
    }

    public BadRequestHandlerResult()
    {
    }
}
