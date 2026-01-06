using ZooApp.Application.ZooKeepers.Results;

namespace ZooApp.Application.ZooKeepers;

public interface IZooKeeperQueryService
{
    Task<ZooKeeperResult> GetZooKeeperByIdAsync(int id);
    Task<List<TaskResult>> GetTaskForZooKeeperAsync(int id, DateTime? form, DateTime? to); 
}
