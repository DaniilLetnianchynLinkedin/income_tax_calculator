using Core.Abstractions.Data.Entities;

namespace Core.Errors.Validation;

public record BaseIdMustBeGreaterThanZeroError<T>() : PropertyValueMustBeGreaterThanZeroError(nameof(T), nameof(IBaseEntity.Id))
    where T : IBaseEntity;
