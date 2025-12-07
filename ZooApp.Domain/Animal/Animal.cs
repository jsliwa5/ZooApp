using System;
using ZooApp.Domain.Animal;
using ZooApp.Domain.Species;
using SpeciesEntity = ZooApp.Domain.Species.Species;

namespace ZooApp.Domain.Animal;

public class Animal
{
    
    public ulong Id { get; init; }
    public string Name { get; private set; }
    public DateTime LastTimeFed { get; private set; }
    public DateTime LastHealthCheck { get; private set; }

    public ulong SpeciesId { get; private set; }

    protected Animal() { }

    //for creating new
    private Animal(string name, ulong speciesId)
    {
    
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Animal name cannot be empty.", nameof(name));

        Name = name;
        SpeciesId = speciesId;

        LastTimeFed = DateTime.UtcNow;
        LastHealthCheck = DateTime.UtcNow;
    }

    public static Animal CreateNew(string name, ulong speciesId)
    {
        return new Animal(name, speciesId);
    }

    public static Animal Restore(ulong id, string name, DateTime lastFed, DateTime lastCheck, ulong speciesId)
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


    //public bool ShouldBeFed(DateTime currentTime)
    //{
        
    //    TimeSpan timeSinceLastFed = currentTime - LastTimeFed;
    //    return timeSinceLastFed.TotalHours >= Species.FeedingIntervalInHours;
    //}

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