using ZooApp.Application.ZooKeepers.Commands;
using ZooApp.Application.ZooKeepers.Results;
using ZooApp.Domain.ZooKeeper;

namespace ZooApp.Application.ZooKeepers.Implementations;

public class ZooKeeperCommandServiceImpl : IZooKeeperCommandService
{

    private readonly IZooKeeperRepository _zooKeeperRepository;

    public ZooKeeperCommandServiceImpl(IZooKeeperRepository zooKeeperRepository)
    {
        _zooKeeperRepository = zooKeeperRepository;
    }

    public async Task<ZooKeeperResult> addZooKeeperAsync(CreateZooKeeperCommand command)
    {

        var zooKeeperToBeAdded = ZooKeeper.CreateNew(
            command.FirstName,
            command.LastName,
            command.MonthlyHoursLimit
        );

        var savedZooKeeper = await _zooKeeperRepository.Save(zooKeeperToBeAdded);

        return new ZooKeeperResult(
                savedZooKeeper.Id,
                savedZooKeeper.FirstName,
                savedZooKeeper.LastName,
                savedZooKeeper.MonthlyHoursLimit
            ); 
    }

    public async Task AsignTaskAutomatically(AsignTaskCommand command)
    {

        await _zooKeeperRepository.CreateAndDispatchTaskAutomatically(
                command.Description,
                command.Duration,
                command.TaskType,
                command.ScheduledAt,
                command.AnimalId
            );

    }
}
