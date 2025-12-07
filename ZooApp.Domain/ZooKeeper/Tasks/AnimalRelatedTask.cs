using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.ZooKeeper.Tasks;

public class AnimalRelatedTask : AbstractTask
{
    
    public int AnimalId { get; private set; }

    public AnimalRelatedTask(int animalId, DateTime scheduledAt, TimeSpan duration, string description)
        : base(scheduledAt, duration, description)
    {
        AnimalId = animalId;
    }

}
