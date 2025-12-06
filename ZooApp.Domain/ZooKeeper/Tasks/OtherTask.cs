using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.ZooKeeper.Tasks;

public class OtherTask : AbstractTask
{
    public OtherTask(DateTime scheduledAt, TimeSpan duration, string description) : base(scheduledAt, duration, description)
    {
    }
}
