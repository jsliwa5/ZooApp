namespace ZooApp.Application.Auth.Results;

public record AuthResult(
    string Token,
    string Role,
    int? DomainId,
    string FirstName,
    string LastName
    );
