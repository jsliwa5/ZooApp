using System;
using ZooApp.Domain.Animal;

namespace ZooApp.Domain.Animal;

public class Animal
{
    
    public ulong Id { get; init; }
    public string Name { get; private set; }
    public DateTime LastTimeFed { get; private set; }
    public DateTime LastHealthCheck { get; private set; }

    public Species Species { get; private set; }
  
    protected Animal() { }

    //for creating new
    private Animal(string name, Species species)
    {
    
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Animal name cannot be empty.", nameof(name));

        if (species is null)
            throw new ArgumentNullException(nameof(species), "Animal must have a species.");

        Name = name;
        Species = species;

        LastTimeFed = DateTime.UtcNow;
        LastHealthCheck = DateTime.UtcNow;
    }

    public static Animal CreateNew(string name, Species species)
    {
        return new Animal(name, species);
    }

    public static Animal Restore(ulong id, string name, DateTime lastFed, DateTime lastCheck, Species species)
    {
        return new Animal
        {
            Id = id,
            Name = name,
            LastTimeFed = lastFed,
            LastHealthCheck = lastCheck,
            Species = species
        };
    }


    public bool ShouldBeFed(DateTime currentTime)
    {
        
        TimeSpan timeSinceLastFed = currentTime - LastTimeFed;
        return timeSinceLastFed.TotalHours >= Species.FeedingIntervalInHours;
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