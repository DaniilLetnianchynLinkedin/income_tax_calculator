namespace Core.Errors.Validation;

public record PropertyValueMustBeGreaterThanError(string EntityName, string PropertyName, int Value)
    : ValidationError($"{PropertyName} value of {EntityName} must be greater than {Value}.");
