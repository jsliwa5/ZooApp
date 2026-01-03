using ZooApp.Application.ZooKeepers.Results;
using ZooApp.Domain.ZooKeeper;

namespace ZooApp.Application.ZooKeepers.Implementations;

public class ZooKeeperQueryServiceImpl : IZooKeeperQueryService
{

    private readonly IZooKeeperRepository _zooKeeperRepository;

    public ZooKeeperQueryServiceImpl(IZooKeeperRepository zooKeeperRepository)
    {
        _zooKeeperRepository = zooKeeperRepository;
    }

    public async Task<ZooKeeperResult> GetZooKeeperByIdAsync(int id)
    {

        var zooKeeper = await _zooKeeperRepository.GetById(id);

        return new ZooKeeperResult(
                zooKeeper.Id,
                zooKeeper.FirstName,
                zooKeeper.LastName,
                zooKeeper.MonthlyHoursLimit
            );
    }
}
