using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Domain.ZooKeeper.Tasks;
using ZooApp.Infrastructure.Persistence;

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

    public Task<ulong> DispatchTaskAutomatically(DateTime scheduledAt, TimeSpan duration, string description)
    {
        throw new NotImplementedException();
    }

    public async Task<ZooKeeper?> GetById(ulong id)
    {
        return await _context.ZooKeepers.FindAsync(id);
    }

    public async Task<List<ITask>> GetTasksForZooKeeper(ulong zooKeeperId)
    {
        return null; // To be implemented
    }

    public Task<List<ITask>> GetTasksForZooKeeperForTheDate(ulong zooKeeperId, DateTime date)
    {
        return null; // To be implemented
    }

    public async Task<ZooKeeper> Save(ZooKeeper zooKeeper)
    {
        await _context.ZooKeepers.AddAsync(zooKeeper);
        await _context.SaveChangesAsync();
        return zooKeeper;
    }
}
