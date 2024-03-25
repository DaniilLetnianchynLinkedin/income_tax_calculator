using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Abstractions.HandlerResults;
using Core.Abstractions.ServiceBus;

namespace Core.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(IMapper mapper, IServiceBus serviceBus)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ServiceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
        }

        protected IServiceBus ServiceBus { get; }

        protected IMapper Mapper { get; }

        protected IActionResult Send<TResponse>(IHandlerResult<TResponse> handlerResult)
            where TResponse : class
        {
            return handlerResult switch {
                ISuccessHandlerResult<TResponse> shr => Ok(Mapper.Map<TResponse>(shr.Response)),
                INotFoundHandlerResult<object> nfhr => string.IsNullOrEmpty(nfhr.AggregateError?.Message) ? string.IsNullOrEmpty(nfhr.Message) ? NotFound() : NotFound(nfhr.Message) : NotFound(nfhr.AggregateError),
                IConflictHandlerResult<object> chr => string.IsNullOrEmpty(chr.AggregateError?.Message) ? string.IsNullOrEmpty(chr.Message) ? Conflict() : Conflict(chr.Message) : Conflict(chr.AggregateError),
                ICreatedHandlerResult<object> chr => StatusCode(StatusCodes.Status201Created, chr.Response),
                IBadRequestHandlerResult<object> brhr => string.IsNullOrEmpty(brhr.AggregateError?.Message) ? BadRequest() : BadRequest(brhr.AggregateError),
                IUnprocessableEntityHandlerResult<object> uehr => StatusCode(StatusCodes.Status422UnprocessableEntity, new { uehr.AggregateError, uehr.PropertiesValidationErrors }),
                _ => throw new ArgumentOutOfRangeException($"Unknown handler result '{handlerResult.GetType()}' encountered."),
            };
        }
    }
}
