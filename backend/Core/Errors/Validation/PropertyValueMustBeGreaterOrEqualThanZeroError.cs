namespace Core.Errors.Validation;

public record PropertyValueMustBeGreaterOrEqualThanZeroError(string EntityName, string PropertyName)
    : PropertyValueMustBeGreaterOrEqualThanError(EntityName, PropertyName, 0);
