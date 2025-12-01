using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.ZooKeeper.Task;

public abstract class AbstractTask : ITask
{
    public ulong Id { get; init; }

    public DateTime ScheduledAt { get; private set; }

    public TimeSpan Duration { get; private set; }

    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }

    public AbstractTask(DateTime scheduledAt, TimeSpan duration, string description)
    {
        if (scheduledAt < DateTime.UtcNow)
            throw new ArgumentException("Cannot schedule task in the past.");
        if (duration <= TimeSpan.Zero)
            throw new ArgumentException("Duration must be positive.", nameof(duration));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        ScheduledAt = scheduledAt;
        Duration = duration;
        Description = description;
        IsCompleted = false;
    }

    public void Complete()
    {
        IsCompleted = true;
    }

    public void Reschedule(DateTime newDate, TimeSpan newDuration)
    {
        if(IsCompleted)
            throw new InvalidOperationException("Cannot reschedule a completed task.");

        if (newDate < DateTime.UtcNow)
            throw new ArgumentException("Cannot schedule task in the past.");

        ScheduledAt = newDate;
        Duration = newDuration;
    }

    public void Uncomplete()
    {
        IsCompleted = false;
    }
}
