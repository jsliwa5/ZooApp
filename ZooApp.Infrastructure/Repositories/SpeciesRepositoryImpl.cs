using Microsoft.EntityFrameworkCore;
using ZooApp.Domain.Species;
using ZooApp.Infrastructure.Persistance;

namespace ZooApp.Infrastructure.Repositories;

public class SpeciesRepositoryImpl : ISpeciesRepository
{

    private readonly ZooDbContext _dbContext;

    public SpeciesRepositoryImpl(ZooDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _dbContext.Species.AnyAsync(s => s.Id == id);
    }

    public async Task<List<Species>> GetAllSpeciesAsync()
    {
        return await _dbContext.Species.ToListAsync();
    }

    public async Task<Species> GetByIdAsync(int id)
    {
        return await _dbContext.Species.FindAsync(id);
    }

    public async Task<Species> SaveSpeciesAsync(Species species)
    {
        _dbContext.Species.Add(species);
        await _dbContext.SaveChangesAsync();
        return species;
    }
}
