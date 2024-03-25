namespace Core.Abstractions.HandlerResults;

public interface INotFoundHandlerResult<out TPayload> : IHandlerResult<TPayload>
    where TPayload : class
{
}