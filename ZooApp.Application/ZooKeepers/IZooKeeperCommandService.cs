using ZooApp.Application.ZooKeepers.Commands;
using ZooApp.Application.ZooKeepers.Results;
using ZooApp.Domain.ZooKeeper;

namespace ZooApp.Application.ZooKeepers;

public interface IZooKeeperCommandService
{

    Task<ZooKeeperResult> addZooKeeperAsync(CreateZooKeeperCommand command);
    

}
