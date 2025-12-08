namespace ZooApp.Domain.Animal;

public class Animal
{
    
    public int Id { get; init; }
    public string Name { get; private set; }
    public DateTime LastTimeFed { get; private set; }
    public DateTime LastHealthCheck { get; private set; }

    public int SpeciesId { get; private set; }

    protected Animal() { }

    //for creating new
    private Animal(string name, int speciesId)
    {
    
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Animal name cannot be empty.", nameof(name));

        Name = name;
        SpeciesId = speciesId;

        LastTimeFed = DateTime.UtcNow;
        LastHealthCheck = DateTime.UtcNow;
    }

    public static Animal CreateNew(string name, int speciesId)
    {
        return new Animal(name, speciesId);
    }

    public static Animal Restore(int id, string name, DateTime lastFed, DateTime lastCheck, int speciesId)
    {
        return new Animal
        {
            Id = id,
            Name = name,
            LastTimeFed = lastFed,
            LastHealthCheck = lastCheck,
            SpeciesId = speciesId
        };
    }


    public bool ShouldBeFed(DateTime currentTime, int feedingIntervalInHours)
    {

        TimeSpan timeSinceLastFed = currentTime - LastTimeFed;
        return timeSinceLastFed.TotalHours >= feedingIntervalInHours;
    }

    public void Feed(DateTime fedAt)
    {
        if (fedAt < LastTimeFed)
            throw new InvalidOperationException("Cannot feed in the past (before last feeding).");

        LastTimeFed = fedAt;
    }

    public bool ShouldHaveHealthCheck(DateTime currentTime, int checkIntervalInDays)
    {
        
        TimeSpan timeSinceLastCheck = currentTime - LastHealthCheck;
        return timeSinceLastCheck.TotalDays >= checkIntervalInDays;
    }

    public void PerformHealthCheck(DateTime checkAt)
    {
        LastHealthCheck = checkAt;
    }
}