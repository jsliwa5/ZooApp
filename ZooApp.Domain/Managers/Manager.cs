namespace ZooApp.Domain.Managers;

public class Manager
{
    public int Id { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Guid UserId { get; private set; }

    //for EF
    private Manager() { }

    //for creating new
    private Manager(string firstName, string lastName, Guid userId)
    {
        FirstName = firstName;
        LastName = lastName;
        UserId = userId;
    }

    //for restoring
    private Manager (int id, string firstName, string lastName, Guid userId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserId = userId;
    }

    public static Manager Restore(int id, string firstName, string lastName,  Guid userId)
    {
        return new Manager(id, firstName, lastName, userId);
    }

    public static Manager CreateNew(string firstName, string lastName, Guid userId)
    {
        return new Manager(firstName, lastName, userId);
    }
}