using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Domain.ZooKeeper.Tasks;
using ZooApp.Infrastructure.Persistance;

namespace ZooApp.Infrastructure.Repositories;

public class ZooKeeperRepositoryImpl : IZooKeeperRepository
{

    private ZooDbContext _context;

    public ZooKeeperRepositoryImpl(ZooDbContext context)
    {
        _context = context;
    }

    public async Task Delete(ZooKeeper zooKeeper)
    {
        _context.ZooKeepers.Remove(zooKeeper);
        await _context.SaveChangesAsync();
    }

    public async Task CreateAndDispatchTaskAutomatically(
        string description,
        TimeSpan duration,
        string taskType,
        DateTime scheduledAt,     
        int? animalId = null
        )
    {

        await _context.Database.ExecuteSqlRawAsync(
            "CALL sp_create_and_assign_task({0}, {1}, {2}, {3}, {4})",
            description,
            duration,
            taskType,
            scheduledAt,
            animalId
            );

        
    }

    public async Task<ZooKeeper?> GetById(int id)
    {
        return await _context.ZooKeepers.FindAsync(id);
    }

    public async Task<List<AbstractTask>> GetTasksForZooKeeper(int zooKeeperId)
    {
        return await _context.Set<AbstractTask>()
        .Where(t => t.ZooKeeperId == zooKeeperId)
        .OrderBy(t => t.ScheduledAt)
        .ToListAsync();
    }

    public async Task<List<AbstractTask>> GetTasksForZooKeeperForThePeriodOfTime(int zooKeeperId, DateTime from, DateTime to)
    {
        return await _context.Set<AbstractTask>()
        .Where(t => t.ZooKeeperId == zooKeeperId
                    && t.ScheduledAt >= from
                    && t.ScheduledAt <= to)
        .OrderBy(t => t.ScheduledAt)
        .ToListAsync();
    }

    public async Task<ZooKeeper> Save(ZooKeeper zooKeeper)
    {
        await _context.ZooKeepers.AddAsync(zooKeeper);
        await _context.SaveChangesAsync();
        return zooKeeper;
    }

    public async Task<bool> ExistById(int id)
    {
        
        return await _context.ZooKeepers.AnyAsync(zk => zk.Id == id);
    }
}
