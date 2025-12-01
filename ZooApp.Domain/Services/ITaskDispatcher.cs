namespace ZooApp.Domain.Services;

public interface ITaskDispatcher
{
    Task<ulong> DispatchTaskAutomaticallyAsync(DateTime scheduledAt, TimeSpan duration, string description);
}
