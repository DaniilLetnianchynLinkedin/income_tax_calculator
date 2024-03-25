namespace Core.Abstractions.HandlerResults;

public interface IConflictHandlerResult<out TPayload> : IHandlerResult<TPayload>
    where TPayload : class
{
}