using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.ZooKeeper.Tasks;

namespace ZooApp.Domain.ZooKeeper;

public class ZooKeeper
{

    public ulong Id { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int MonthlyHoursLimit { get; set; }

    private List<AbstractTask> _tasks;
    public IReadOnlyCollection<AbstractTask> Tasks => _tasks.AsReadOnly();


    //for creating new
    private ZooKeeper(string firstName, string lastName, int monthlyHoursLimit)
    {

        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentNullException(nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentNullException(nameof(lastName));
        if (monthlyHoursLimit <= 0) throw new ArgumentException("Limit must be positive");
        FirstName = firstName;
        LastName = lastName;
        MonthlyHoursLimit = monthlyHoursLimit;
        _tasks = new List<AbstractTask>();
    }

    //for restoring
    private ZooKeeper(ulong id, string firstName, string lastName, int monthlyHoursLimit, List<AbstractTask> tasks)
        : this(firstName, lastName, monthlyHoursLimit)
    {

        Id = id;
        _tasks = tasks;
    }

    public static ZooKeeper CreateNew(string firstName, string lastName, int monthlyHoursLimit)
    {
        return new ZooKeeper(firstName, lastName, monthlyHoursLimit);
    }

    public static ZooKeeper Restore(ulong id, string firstName, string lastName,
        int monthlyHoursLimit, List<AbstractTask> tasks)
    {

        return new ZooKeeper(
                id,
                firstName,
                lastName,
                monthlyHoursLimit,
                tasks
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
