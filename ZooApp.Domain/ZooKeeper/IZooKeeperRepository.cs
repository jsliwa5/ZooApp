using ZooApp.Domain.ZooKeeper.ReadModels;
using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Domain.ZooKeeper;

public interface IZooKeeperRepository
{
    Task<ZooKeeper?> GetById(int id);
    Task<bool> ExistById(int id);
    Task CreateAndDispatchTaskAutomatically(
        string description,
        TimeSpan duration,
        string taskType,
        DateTime scheduledAt,     
        int? animalId = null
        ); 
    Task<ZooKeeper> Save(ZooKeeper zooKeeper);
    Task Delete(ZooKeeper zooKeeper);
    Task<List<AbstractTask>> GetTasksForZooKeeper(int zooKeeperId);
    Task<List<AbstractTask>> GetTasksForZooKeeperForThePeriodOfTime(int zooKeeperId, DateTime form, DateTime to);

    Task<ZooKeeperWithLoad?> GetZooKeeperWithLoadAsync(int id, int month, int year);
}
