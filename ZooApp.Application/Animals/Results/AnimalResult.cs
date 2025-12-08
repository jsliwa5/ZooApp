using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Application.Animals.Results;

public record AnimalResult
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateTime LastTimeFed { get; init; }
    public DateTime LastHealthCheck { get; init; }
    public int SpeciesId { get; init; }
    
    public AnimalResult(int id, string name, DateTime lastTimeFed, DateTime lastHealthCheck, int speciesId)
    {
        Id = id;
        Name = name;
        LastTimeFed = lastTimeFed;
        LastHealthCheck = lastHealthCheck;
        SpeciesId = speciesId;
    }
}
