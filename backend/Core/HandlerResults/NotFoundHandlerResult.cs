using Core.Abstractions.Errors;
using Core.Abstractions.HandlerResults;

namespace Core.HandlerResults;

public class NotFoundHandlerResult<TPayload> : HandlerResult<TPayload>, INotFoundHandlerResult<TPayload>
    where TPayload : class
{
    public NotFoundHandlerResult()
    {
    }

    public NotFoundHandlerResult(string message)
        : base(message)
    {
    }

    public NotFoundHandlerResult(IError error)
        : base(error)
    {
    }

    public NotFoundHandlerResult(TPayload response, string message)
        : base(response, message)
    {
    }
}
