using ZooApp.Application.ZooKeepers.Results;
using ZooApp.Domain.ZooKeeper;
using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Application.ZooKeepers.Implementations;

public class ZooKeeperQueryServiceImpl : IZooKeeperQueryService
{

    private readonly IZooKeeperRepository _zooKeeperRepository;

    public ZooKeeperQueryServiceImpl(IZooKeeperRepository zooKeeperRepository)
    {
        _zooKeeperRepository = zooKeeperRepository;
    }

    public async Task<List<TaskResult>> GetTaskForZooKeeperAsync(int id, DateTime? from, DateTime? to)
    {
        if(!await _zooKeeperRepository.ExistById(id))
        {
            throw new Exception($"ZooKeeper with id {id} does not exist.");
        }

        var tasks = new List<AbstractTask>();

        if ( (from is null) || (to is null))
        {
            tasks = await _zooKeeperRepository.GetTasksForZooKeeper(id);
        }
        else
        {
            var utcFrom = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);
            var utcTo = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);

            tasks = await _zooKeeperRepository.GetTasksForZooKeeperForThePeriodOfTime(id, utcFrom, utcTo);
        }

        return tasks.Select(t => new TaskResult(
                t.Id,
                t.ScheduledAt,
                t.Duration,
                t.Description,
                t.IsCompleted,
                t.GetType().Name,
                t is AnimalRelatedTask animalTask ? animalTask.AnimalId : null
            )
        ).ToList();
    }

    public async Task<ZooKeeperResult> GetZooKeeperByIdAsync(int id)
    {
        
        var currentMonth = DateTime.UtcNow.Month;
        var currentYear = DateTime.UtcNow.Year;

        var data = await _zooKeeperRepository.GetZooKeeperWithLoadAsync(id, currentMonth, currentYear);

        if (data == null)
        {
            throw new Exception($"ZooKeeper with id {id} does not exist.");
        }

        return new ZooKeeperResult(
            data.Id,
            data.FirstName,
            data.LastName,
            data.MonthlyHoursLimit,
            data.CurrentLoad
        );
    }
}
