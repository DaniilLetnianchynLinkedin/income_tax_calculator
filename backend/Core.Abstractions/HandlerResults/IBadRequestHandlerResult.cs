namespace Core.Abstractions.HandlerResults;

public interface IBadRequestHandlerResult<out TPayload> : IHandlerResult<TPayload>
    where TPayload : class
{
}