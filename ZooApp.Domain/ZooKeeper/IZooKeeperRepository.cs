using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.ZooKeeper.Task;

namespace ZooApp.Domain.ZooKeeper;

public interface IZooKeeperRepository
{
    ZooKeeper? GetById(ulong id);
    Task<ulong> DispatchTaskAutomatically(DateTime scheduledAt, TimeSpan duration, string description); //tu będzie funkcja/procedura składowana do bazy danych
    ZooKeeper Save(ZooKeeper zooKeeper);
    void Delete(ZooKeeper zooKeeper);
    List<ITask> GetTasksForZooKeeper(ulong zooKeeperId);
    List<ITask> GetTasksForZooKeeperForTheDate(ulong zooKeeperId, DateTime date);
}
