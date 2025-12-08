using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Application.Species.Commands;

public record CreateSpeciesCommand
{
    public string Name { get; init; }
    public int FeedingIntervalInHours { get; init; }
    public string Kingdom { get; init; }

    public CreateSpeciesCommand(string name, int feedingIntervalInHours, string kingdom)
    {
        Name = name;
        FeedingIntervalInHours = feedingIntervalInHours;
        Kingdom = kingdom;
    }
}
