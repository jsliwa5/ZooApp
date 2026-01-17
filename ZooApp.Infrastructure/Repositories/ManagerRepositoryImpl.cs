using Microsoft.EntityFrameworkCore;
using ZooApp.Domain.Managers;
using ZooApp.Infrastructure.Persistance;

namespace ZooApp.Infrastructure.Repositories;

public class ManagerRepositoryImpl : IManagerRepository
{
    private readonly ZooDbContext _dbContext;

    public ManagerRepositoryImpl(ZooDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteAsync(Manager manager)
    {
        _dbContext.Managers.Remove(manager);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _dbContext.Managers.AnyAsync(m => m.Id == id);
    }

    public async Task<List<Manager>> GetAllManagersAsync()
    {
        return await _dbContext.Managers.ToListAsync();
    }

    public async Task<Manager?> GetManagerByIdAsync(int id)
    {
        return await _dbContext.Managers.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Manager> SaveAsync(Manager manager)
    {
        _dbContext.Managers.Add(manager);
        await _dbContext.SaveChangesAsync();
        return manager;
    }
}