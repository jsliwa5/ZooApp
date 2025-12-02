using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.ZooKeeper.Tasks;

public interface ITask
{
    ulong Id { get; init; }
    ulong ZooKeeperId { get; } // some people say that id breaks the DDD principles, some say it doesn't, but surely it makes mapping to DB easier
    DateTime ScheduledAt { get;}
    TimeSpan Duration { get;}
    string Description { get;}
    bool IsCompleted { get;}


    void Complete();
    void Uncomplete();
    void Reschedule(DateTime newDate, TimeSpan newDuration);
}
