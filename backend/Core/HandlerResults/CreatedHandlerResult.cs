using Core.Abstractions.HandlerResults;

namespace Core.HandlerResults;

public class CreatedHandlerResult<TPayload> : HandlerResult<TPayload>, ICreatedHandlerResult<TPayload>
    where TPayload : class
{
    public CreatedHandlerResult(TPayload response)
        : base(response)
    {
    }
}
