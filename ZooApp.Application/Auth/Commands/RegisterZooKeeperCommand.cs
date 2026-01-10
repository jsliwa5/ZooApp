namespace ZooApp.Application.Auth.Commands;

public record RegisterZooKeeperCommand (
    string Email,
    string Password,
    string FirstName,
    string LastName,
    int HoursLimit
);
