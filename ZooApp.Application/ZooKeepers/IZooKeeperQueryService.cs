using ZooApp.Application.ZooKeepers.Results;

namespace ZooApp.Application.ZooKeepers;

public interface IZooKeeperQueryService
{
    Task<ZooKeeperResult> GetZooKeeperByIdAsync(int id);
}
