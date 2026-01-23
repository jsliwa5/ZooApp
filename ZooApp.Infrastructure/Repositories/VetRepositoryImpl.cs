using Microsoft.EntityFrameworkCore;
using ZooApp.Domain.Vets;
using ZooApp.Infrastructure.Persistance;

namespace ZooApp.Infrastructure.Repositories;

public class VetRepositoryImpl : IVetRepository
{

    private readonly ZooDbContext _context;

    public VetRepositoryImpl(ZooDbContext context)
    {
        _context = context;
    }

    public async Task DeleteAsync(Vet vet)
    {
        _context.Vets.Remove(vet);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _context.Vets.AnyAsync(v => v.Id == id);
    }

    public async Task<List<Vet>> GetAllVetsAsync()
    {
        return await _context.Vets.AsNoTracking().ToListAsync();
    }

    public async Task<Vet?> GetVetByIdAsync(int id)
    {
        return await _context.Vets.FindAsync(id);
    }

    public async Task<Vet?> GetVetWithVisitsByIdAsync(int id)
    {
        return await _context.Vets
            .Include(v => v.Visits)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Visit>> GetVisitsForVetAsync(int vetId)
    {
        return await _context.Set<Visit>()
            .Where(v => v.VetId == vetId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Visit>> GetVisitsForVisitForTheDate(int vetId, DateTime date)
    {
        return await _context.Set<Visit>()
            .Where(v => v.VetId == vetId && v.ScheduledAt.Date == date.Date)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Vet> SaveAsync(Vet vet)
    {

        if (vet.Id == 0)
        {
            await _context.Vets.AddAsync(vet);
        }
        else
        {
            _context.Vets.Update(vet);
        }

        await _context.SaveChangesAsync();
        return vet;
    }
}
