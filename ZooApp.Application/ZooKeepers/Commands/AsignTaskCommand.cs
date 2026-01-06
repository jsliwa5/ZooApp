namespace ZooApp.Application.ZooKeepers.Commands;

public record AsignTaskCommand
{
    public string Description { get; init; }
    public TimeSpan Duration { get; init; }
    public string TaskType { get; init; }
    public DateTime ScheduledAt { get; init; }
    public int? AnimalId { get; init; }

    public AsignTaskCommand(string description, TimeSpan duration, string taskType, DateTime scheduledAt, int? animalId)
    {
        Description = description;
        Duration = duration;
        TaskType = taskType;
        ScheduledAt = scheduledAt;
        AnimalId = animalId;
    }
}
