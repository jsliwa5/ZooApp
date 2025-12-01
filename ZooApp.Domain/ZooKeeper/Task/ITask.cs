using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.ZooKeeper.Task;

public interface ITask
{
    ulong Id { get; init; }
    DateTime ScheduledAt { get;}
    TimeSpan Duration { get;}
    string Description { get;}
    bool IsCompleted { get;}

    void Complete();
    void Uncomplete();
    void Reschedule(DateTime newDate, TimeSpan newDuration);
}
