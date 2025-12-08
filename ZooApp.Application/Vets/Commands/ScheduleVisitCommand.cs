using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Application.Vets.Commands;

public record ScheduleVisitCommand
{
    public int AnimalId { get; init; }
    public DateTime ScheduledAt { get; init; }
    
    public int DurationInHours { get; init; }
    public string Description { get; init; }

    public ScheduleVisitCommand(int animalId, DateTime scheduledAt, int durationInHours, string description)
    {
        AnimalId = animalId;
        ScheduledAt = scheduledAt;
        DurationInHours = durationInHours;
        Description = description;
    }
}
