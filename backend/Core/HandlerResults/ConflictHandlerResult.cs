using Core.Abstractions.Errors;
using Core.Abstractions.HandlerResults;

namespace Core.HandlerResults;

public class ConflictHandlerResult<TPayload> : HandlerResult<TPayload>, IConflictHandlerResult<TPayload>
    where TPayload : class
{
    public ConflictHandlerResult(string message)
        : base(message)
    {
    }

    public ConflictHandlerResult(IError error)
        : base(error)
    {
    }
}
