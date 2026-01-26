namespace ZooApp.Domain.ZooKeeper.ReadModels;

public record ZooKeeperWithLoad(
    int Id,
    string FirstName,
    string LastName,
    int MonthlyHoursLimit,
    double CurrentLoad
    );

