using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Animal;

namespace ZooApp.Application.Commands;

public record CreateAnimalCommand
{
    public string Name { get; init; }
    public int SpeciesId { get; init; }

    public CreateAnimalCommand(string name, int speciesId)
    {
        Name = name;
        SpeciesId = speciesId;
    }

}
