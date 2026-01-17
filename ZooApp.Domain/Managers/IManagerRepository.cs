namespace ZooApp.Domain.Managers;

public interface IManagerRepository
{
    Task<Manager?> GetManagerByIdAsync(int id);
    Task<List<Manager>> GetAllManagersAsync();
    Task<Manager> SaveAsync(Manager manager);
    Task DeleteAsync(Manager manager);
    Task<bool> ExistsByIdAsync(int id);
}
