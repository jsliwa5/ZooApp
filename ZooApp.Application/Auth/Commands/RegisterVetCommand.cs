namespace ZooApp.Application.Auth.Commands;

public record RegisterVetCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    int HoursLimit
);

