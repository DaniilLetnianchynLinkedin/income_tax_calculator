namespace Core.Errors.Validation;

public record InvalidPropertiesValueError(string Message, params string[] PropertyNames) :
    ValidationError($"Parameters {string.Join(" and", PropertyNames)} has incorrect value. {Message}");