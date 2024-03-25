namespace Core.Errors.Validation;

public record InvalidPropertyValueError(string PropertyName, string Message) :
    ValidationError($"{PropertyName} has incorrect value. {Message}");