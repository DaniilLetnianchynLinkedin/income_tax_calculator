namespace Core.Abstractions.HandlerResults;

public interface ICreatedHandlerResult<out TPayload> : IHandlerResult<TPayload>
    where TPayload : class
{
}