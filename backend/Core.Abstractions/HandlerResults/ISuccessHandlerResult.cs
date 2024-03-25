namespace Core.Abstractions.HandlerResults;

public interface ISuccessHandlerResult<out TPayload> : IHandlerResult<TPayload>
    where TPayload : class
{
}