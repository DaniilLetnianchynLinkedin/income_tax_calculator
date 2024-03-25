using Core.Abstractions.Data.Entities;
using Core.Errors.Validation;

namespace Core.Errors;

public abstract class BaseEntityErrors<T>
    where T : IBaseEntity
{
    public record NotFoundError : BaseEntityNotFoundByIdError<T>;

    public record IdMustBeGreaterThanZeroError : BaseIdMustBeGreaterThanZeroError<T>;
}
