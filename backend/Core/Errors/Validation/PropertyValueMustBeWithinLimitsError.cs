namespace Core.Errors.Validation;

public record PropertyValueMustBeWithinLimitsError(string EntityName, string PropertyName, int MinValue, int MaxValue)
    : ValidationError($"{PropertyName} value of {EntityName} must be greater than {MinValue} and less than {MaxValue}.");
