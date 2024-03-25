using Core.Abstractions.HandlerResults;

namespace Core.HandlerResults;

public class SuccessHandlerResult<TPayload> : HandlerResult<TPayload>, ISuccessHandlerResult<TPayload>
    where TPayload : class
{
    public SuccessHandlerResult()
    {
        Success = true;
    }

    public SuccessHandlerResult(TPayload response)
        : base(response)
    {
        Success = true;
    }
}
