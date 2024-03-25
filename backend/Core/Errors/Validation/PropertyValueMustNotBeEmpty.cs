namespace Core.Errors.Validation;

public record PropertyValueMustNotBeEmpty(string EntityName, string PropertyName)
    : ValidationError($"{PropertyName} value of {EntityName} must not be empty.");
