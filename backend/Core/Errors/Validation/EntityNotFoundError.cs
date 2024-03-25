namespace Core.Errors.Validation;

public record EntityNotFoundError(string EntityName, params string[] IdentifierNames) :
    ValidationError($"{EntityName} with such {string.Join(" and", IdentifierNames)} was not found.");