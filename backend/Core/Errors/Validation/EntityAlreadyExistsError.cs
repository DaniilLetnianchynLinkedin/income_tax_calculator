namespace Core.Errors.Validation;

public record EntityAlreadyExistsError(string EntityName, params string[] IdentifierNames) :
    ValidationError($"{EntityName} with such {string.Join(" and", IdentifierNames)} already exists.");