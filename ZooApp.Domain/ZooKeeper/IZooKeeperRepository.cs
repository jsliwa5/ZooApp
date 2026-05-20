using ZooApp.Domain.ZooKeeper.ReadModels;
using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Domain.ZooKeeper;

public interface IZooKeeperRepository
{
    Task<ZooKeeper?> GetByIdAsync(int id);
    Task<bool> ExistByIdAsync(int id);
    Task CreateAndDispatchTaskAutomaticallyAsync(
        string description,
        TimeSpan duration,
        string taskType,
        DateTime scheduledAt,     
        int? animalId = null
        ); 
    Task<ZooKeeper> SaveAsync(ZooKeeper zooKeeper);
    Task Delete(ZooKeeper zooKeeper);
    Task<List<AbstractTask>> GetTasksForZooKeeperAsync(int zooKeeperId);
    Task<List<AbstractTask>> GetTasksForZooKeeperForThePeriodOfTimeAsync(int zooKeeperId, DateTime form, DateTime to);

    Task<ZooKeeperWithLoad?> GetZooKeeperWithLoadAsync(int id, int month, int year);
}
