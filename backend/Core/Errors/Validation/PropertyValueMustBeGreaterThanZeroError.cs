namespace Core.Errors.Validation;

public record PropertyValueMustBeGreaterThanZeroError(string EntityName, string PropertyName)
    : PropertyValueMustBeGreaterThanError(EntityName, PropertyName, 0);