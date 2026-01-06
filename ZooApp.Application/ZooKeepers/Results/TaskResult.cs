namespace ZooApp.Application.ZooKeepers.Results;

public record TaskResult
{
    public int Id { get; init; }
    public DateTime ScheduledAt { get; init; }
    public TimeSpan Duration { get; init; }
    public string Description { get; init; }
    public bool IsCompleted { get; init; }
    public String TaskType { get; init; } = "Other";
    public int? animalId { get; init; }

    public TaskResult(int id, DateTime scheduledAt, TimeSpan duration, string description, bool isCompleted, string taskType, int? animalId)
    {
        Id = id;
        ScheduledAt = scheduledAt;
        Duration = duration;
        Description = description;
        IsCompleted = isCompleted;
        TaskType = taskType;
        this.animalId = animalId;
    }
}
