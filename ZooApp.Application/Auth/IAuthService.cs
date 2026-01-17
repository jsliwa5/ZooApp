using ZooApp.Application.Auth.Results;

namespace ZooApp.Application.Auth;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string email, string password);
    Task RegisterZooKeeperAsync(string email, string password, string firstName, string lastName, int hoursLimit);
    Task RegisterVetAsync(string email, string password, string firstName, string lastName, int monthlyHoursLimit);

    Task RegisterManagerAsync(string email, string password, string firstName, string lastName);
}
