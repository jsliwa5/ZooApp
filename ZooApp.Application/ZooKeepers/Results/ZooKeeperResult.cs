namespace ZooApp.Application.ZooKeepers.Results;

//without tasks
public record ZooKeeperResult
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int MonthlyHoursLimit { get; init; }

    public ZooKeeperResult(int id, string firstName, string lastName, int monthlyHoursLimit)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MonthlyHoursLimit = monthlyHoursLimit;
    }
}
