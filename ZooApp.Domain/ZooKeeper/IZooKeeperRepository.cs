using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Domain.ZooKeeper;

public interface IZooKeeperRepository
{
    Task<ZooKeeper?> GetById(int id);
    Task<int> DispatchTaskAutomatically(DateTime scheduledAt, TimeSpan duration, string description); //tu będzie funkcja/procedura składowana do bazy danych
    Task<ZooKeeper> Save(ZooKeeper zooKeeper);
    Task Delete(ZooKeeper zooKeeper);
    Task<List<ITask>> GetTasksForZooKeeper(int zooKeeperId);
    Task<List<ITask>> GetTasksForZooKeeperForTheDate(int zooKeeperId, DateTime date);
}
