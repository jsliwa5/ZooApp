namespace ZooApp.Application.ZooKeepers.Commands;

public record CreateZooKeeperCommand
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int MonthlyHoursLimit { get; init; }
    public CreateZooKeeperCommand(string firstName, string lastName, int monthlyHoursLimit)
    {
        FirstName = firstName;
        LastName = lastName;
        MonthlyHoursLimit = monthlyHoursLimit;
    }
}
