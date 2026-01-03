using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Domain.ZooKeeper;

public interface IZooKeeperRepository
{
    Task<ZooKeeper?> GetById(int id);
    Task CreateAndDispatchTaskAutomatically(
        string description,
        TimeSpan duration,
        string taskType,
        DateTime scheduledAt,     
        int? animalId = null
        ); 
    Task<ZooKeeper> Save(ZooKeeper zooKeeper);
    Task Delete(ZooKeeper zooKeeper);
    Task<List<ITask>> GetTasksForZooKeeper(int zooKeeperId);
    Task<List<ITask>> GetTasksForZooKeeperForTheDate(int zooKeeperId, DateTime date);
}
