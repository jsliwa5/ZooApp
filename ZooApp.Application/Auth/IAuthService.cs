namespace ZooApp.Application.Auth;

public interface IAuthService
{
    Task<string> LoginAsync(string email, string password);
    Task RegisterZooKeeperAsync(string email, string password, string firstName, string lastName, int hoursLimit);
    Task RegisterVetAsync(string email, string password, string firstName, string lastName, int monthlyHoursLimit);
}
