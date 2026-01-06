using ZooApp.Application.ZooKeepers.Commands;
using ZooApp.Application.ZooKeepers.Results;

namespace ZooApp.Application.ZooKeepers;

public interface IZooKeeperCommandService
{

    Task<ZooKeeperResult> addZooKeeperAsync(CreateZooKeeperCommand command);
    Task AsignTaskAutomatically(AsignTaskCommand command);
    

}
