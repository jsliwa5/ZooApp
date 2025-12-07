using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Application.Commands;

public class CreateSpeciesCommand
{
    public string Name { get; set; }
    public int FeedingIntervalInHours { get; set; }
    public string Kingdom { get; set; }

    public CreateSpeciesCommand(string name, int feedingIntervalInHours, string kingdom)
    {
        Name = name;
        FeedingIntervalInHours = feedingIntervalInHours;
        Kingdom = kingdom;
    }
}
