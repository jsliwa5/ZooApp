using System;

namespace ZooApp.Domain.Vet;

public class Visit
{
    public ulong Id { get; init; }
    public ulong AnimalId { get; init; }
    public string Description { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public int DurationInHours { get; private set; }

    public bool IsCompleted { get; private set; }

    internal Visit(ulong animalId, string description, DateTime scheduledAt, int durationInHours)
    {
        if (durationInHours <= 0)
            throw new ArgumentException("Duration must be positive.", nameof(durationInHours));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        AnimalId = animalId;
        Description = description;
        ScheduledAt = scheduledAt;
        DurationInHours = durationInHours;
        IsCompleted = false;
    }

    protected Visit() { }

    public void Perform()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Visit is already completed.");

        IsCompleted = true;
    }

    public void Reschedule(DateTime newDate)
    {
        if (IsCompleted)
            throw new InvalidOperationException("Cannot reschedule a completed visit.");

        if (newDate < DateTime.UtcNow)
            throw new ArgumentException("Cannot schedule visit in the past.");

        ScheduledAt = newDate;
    }
}