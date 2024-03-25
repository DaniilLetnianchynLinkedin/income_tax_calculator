namespace Core.Errors.Validation;

public record PropertyValueMustBeGreaterOrEqualThanError(string EntityName, string PropertyName, int Value)
    : ValidationError($"{PropertyName} value of {EntityName} must be greater or equal than {Value}.");
