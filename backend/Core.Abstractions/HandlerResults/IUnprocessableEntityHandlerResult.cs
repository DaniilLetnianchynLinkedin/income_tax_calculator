namespace Core.Abstractions.HandlerResults;

public interface IUnprocessableEntityHandlerResult<out TPayload> : IHandlerResult<TPayload>
    where TPayload : class
{
}