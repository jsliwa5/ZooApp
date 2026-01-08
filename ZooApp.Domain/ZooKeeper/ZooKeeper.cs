using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Domain.ZooKeeper;

public class ZooKeeper
{

    public int Id { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int MonthlyHoursLimit { get; set; }

    private List<AbstractTask> _tasks;
    public IReadOnlyCollection<AbstractTask> Tasks => _tasks.AsReadOnly();

    public Guid UserId { get; private set; }


    //for creating new
    private ZooKeeper(string firstName, string lastName, int monthlyHoursLimit, Guid userId)
    {

        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentNullException(nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentNullException(nameof(lastName));
        if (monthlyHoursLimit <= 0) throw new ArgumentException("Limit must be positive");
        FirstName = firstName;
        LastName = lastName;
        MonthlyHoursLimit = monthlyHoursLimit;
        UserId = userId;
        _tasks = new List<AbstractTask>();
    }

    //for restoring
    private ZooKeeper(int id, string firstName, string lastName, int monthlyHoursLimit, List<AbstractTask> tasks, Guid userId)
        : this(firstName, lastName, monthlyHoursLimit, userId)
    {

        Id = id;
        _tasks = tasks;
    }

    public static ZooKeeper CreateNew(string firstName, string lastName, int monthlyHoursLimit, Guid userId)
    {
        return new ZooKeeper(firstName, lastName, monthlyHoursLimit, userId);
    }

    public static ZooKeeper Restore(int id, string firstName, string lastName,
        int monthlyHoursLimit, List<AbstractTask> tasks, Guid userId)
    {

        return new ZooKeeper(
                id,
                firstName,
                lastName,
                monthlyHoursLimit,
                tasks,
                userId
            );
    }

    public void AssignTask(AbstractTask newTask)
    {

        var hoursUsedThisMonth = _tasks
        .Where(t => t.ScheduledAt.Year == newTask.ScheduledAt.Year && t.ScheduledAt.Month == newTask.ScheduledAt.Month)
        .Sum(t => t.Duration.TotalHours);

        if (hoursUsedThisMonth + newTask.Duration.TotalHours > MonthlyHoursLimit)
        {
            throw new InvalidOperationException("Przekroczono limit godzin!");
        }

        _tasks.Add(newTask);
    }



}
