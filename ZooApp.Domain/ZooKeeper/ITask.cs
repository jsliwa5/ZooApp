using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.ZooKeeper;

public interface ITask
{
    ulong Id { get; init; }
    DateTime From { get; set; }
    DateTime To { get; set; }
    string Description { get; set; }
    bool IsCompleted { get; set; }

    bool ChangeStatus();
}
