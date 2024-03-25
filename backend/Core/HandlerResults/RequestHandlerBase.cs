using MediatR;
using Core.Abstractions.Errors;
using Core.Abstractions.HandlerResults;

namespace Core.HandlerResults;

public abstract class RequestHandlerBase<TRequest, TPayload> : IRequestHandler<TRequest, IHandlerResult<TPayload>>
    where TRequest : IRequest<IHandlerResult<TPayload>>
    where TPayload : class
{
    public abstract Task<IHandlerResult<TPayload>> Handle(TRequest query, CancellationToken cancellationToken);

    protected static IHandlerResult<TData> Empty<TData>()
        where TData : class => new SuccessHandlerResult<TData>();

    protected static IHandlerResult<TData> NotFound<TData>()
        where TData : class => new NotFoundHandlerResult<TData>();

    protected static IHandlerResult<TData> NotFound<TData>(string message)
        where TData : class => new NotFoundHandlerResult<TData>(message);

    protected static IHandlerResult<TData> NotFound<TData>(IError error)
        where TData : class => new NotFoundHandlerResult<TData>(error);

    protected static IHandlerResult<TData> Data<TData>(TData data)
        where TData : class => new SuccessHandlerResult<TData>(data);

    protected static IHandlerResult<TData> Conflict<TData>(IError error)
        where TData : class => new ConflictHandlerResult<TData>(error);

    protected static IHandlerResult<TData> Conflict<TData>(string message)
        where TData : class => new ConflictHandlerResult<TData>(message);

    protected static IHandlerResult<TData> Created<TData>(TData data)
        where TData : class => new CreatedHandlerResult<TData>(data);

    protected static IHandlerResult<TData> BadRequest<TData>()
        where TData : class => new BadRequestHandlerResult<TData>();

    protected static IHandlerResult<TData> BadRequest<TData>(IError error)
        where TData : class => new BadRequestHandlerResult<TData>(error);

    protected static IHandlerResult<TData> UnprocessableEntity<TData>()
        where TData : class => new UnprocessableEntityHandlerResult<TData>();
}
