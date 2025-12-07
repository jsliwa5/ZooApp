using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Vet;
using ZooApp.Infrastructure.Persistence;

namespace ZooApp.Infrastructure.Repositories;

public class VetRepositoryImpl : IVetRepository
{

    private readonly ZooDbContext _context;

    public VetRepositoryImpl(ZooDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Vet vet)
    {
        _context.Vets.Remove(vet);
        await _context.SaveChangesAsync();
    }

    public async Task<Vet?> GetVetById(int id)
    {
        return await _context.Vets.FindAsync(id);
    }

    public async Task<List<Visit>> GetVisitsForVet(int vetId)
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

    public async Task<Vet> Save(Vet vet)
    {
        await _context.Vets.AddAsync(vet);
        return vet;
    }
}
