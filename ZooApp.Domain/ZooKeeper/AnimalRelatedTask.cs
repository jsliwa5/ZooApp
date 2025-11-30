using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.ZooKeeper;

public class AnimalRelatedTask : ITask
{
    public ulong Id { get; init; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public ulong AnimalId { get; private set; }

    public bool ChangeStatus()
    {
       IsCompleted = !IsCompleted;
        return IsCompleted;
    }

    public AnimalRelatedTask(ulong animalId, DateTime from, DateTime to, string description)
    {
        if (from >= to)
            throw new ArgumentException("Start time must be before end time.");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        AnimalId = animalId;
        this.From = from;
        this.To = to;
        Description = description;
        IsCompleted = false;
    }




}
