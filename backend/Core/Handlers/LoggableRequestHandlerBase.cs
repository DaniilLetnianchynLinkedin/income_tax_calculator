using MediatR;
using Microsoft.AspNetCore.Http;
using Core.Abstractions.HandlerResults;
using Core.Abstractions.Logging;
using Core.HandlerResults;

namespace Core.Handlers;

public abstract class LoggableRequestHandlerBase<TRequest, TResponse> :
    RequestHandlerBase<TRequest, TResponse>
    where TRequest : IRequest<IHandlerResult<TResponse>>
    where TResponse : class
{
    protected readonly ILogger Logger;
    protected readonly IHttpContextAccessor HttpContextAccessor;

    protected LoggableRequestHandlerBase(ILogger logger, IHttpContextAccessor httpContextAccessor)
    {
        Logger = logger;
        HttpContextAccessor = httpContextAccessor;
    }
}