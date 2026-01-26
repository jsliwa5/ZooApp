namespace ZooApp.Application.ZooKeepers.Results;

//without tasks
public record ZooKeeperResult (
    int Id,
    string FirstName,
    string LastName,
    int MonthlyHoursLimit,
    double Load = 0.0
    );

    

    

