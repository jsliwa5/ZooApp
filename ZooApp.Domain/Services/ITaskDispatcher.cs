namespace ZooApp.Domain.Services;

public interface ITaskDispatcher
{
    Task<int> DispatchTaskAutomaticallyAsync(DateTime scheduledAt, TimeSpan duration, string description);
}
