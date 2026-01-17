namespace ZooApp.Application.Auth.Commands;

public record RegisterManagerCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName
);

