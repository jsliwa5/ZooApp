namespace ZooApp.Application.Vets.Results;

public record VisitResult(
    int Id,
    int AnimalId,
    string Description,
    DateTime ScheduledAt,
    int DurationInHours,
    bool IsCompleted
    );

