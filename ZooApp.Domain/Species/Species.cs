using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Animal;

namespace ZooApp.Domain.Species;

public enum AnimalKingdom
{
    Mammal,
    Bird,
    Fish,
    Reptile,
    Amphibian
}

public class Species 
{
    public ulong Id { get; init; } 
    public string Name { get; private set; }
    public int FeedingIntervalInHours { get; private set; }
    public AnimalKingdom Kingdom { get; private set; }

    // for ORM
    protected Species() { }

    // for creating new
    public static Species CreateNew(string name, int feedingInterval, AnimalKingdom kingdom)
    {
        return new Species(name, feedingInterval, kingdom);
    }

    private Species(string name, int feedingInterval, AnimalKingdom kingdom)
    {
        Name = name;
        FeedingIntervalInHours = feedingInterval;
        Kingdom = kingdom;
    }

    public static Species Restore(ulong id, string name, int feedingInterval, AnimalKingdom kingdom)
    {
        return new Species
        {
            Id = id,
            Name = name,
            FeedingIntervalInHours = feedingInterval,
            Kingdom = kingdom
        };
    }

    public void UpdateFeedingProtocol(int newInterval)
    {
        if (newInterval <= 0) throw new ArgumentException("...");
        FeedingIntervalInHours = newInterval;
    }
}
