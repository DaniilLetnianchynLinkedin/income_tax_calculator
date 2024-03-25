using FluentValidation;
using MediatR;
using Core.Abstractions.Errors;
using Core.Errors;
using Core.HandlerResults;

#pragma warning disable CS8603

namespace Core.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => this.validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var errorsDictionary = validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => new Error(x.ErrorMessage, x.ErrorCode) as IError,
                (propertyName, errors) => new
                {
                    Key = propertyName,
                    Errors = errors.DistinctBy(x => x.GetType()),
                })
            .ToDictionary(x => x.Key, x => x.Errors);

        if (!errorsDictionary.Any())
        {
            return await next();
        }

        var responseType = typeof(TResponse);
        if (responseType.IsGenericType)
        {
            var resultType = responseType.GetGenericArguments()[0];
            var unprocessableEntityHandlerResultResponseType = typeof(UnprocessableEntityHandlerResult<>).MakeGenericType(resultType);
            var error = Activator.CreateInstance(unprocessableEntityHandlerResultResponseType, Errors.Errors.Aggregate.ValidationError, errorsDictionary);
            var response = error as TResponse;
            return response;
        }

        return await next();
    }
}
