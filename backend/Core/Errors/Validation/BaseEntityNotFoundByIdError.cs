using Core.Abstractions.Data.Entities;
using Core.Data;

namespace Core.Errors.Validation;

public record BaseEntityNotFoundByIdError<T>() : EntityNotFoundError(typeof(T).Name, nameof(IBaseEntity.Id))
    where T : IBaseEntity;

public record BaseEntityNotFoundByIdError() : BaseEntityNotFoundByIdError<BaseEntity>();