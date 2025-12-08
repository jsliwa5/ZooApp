using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Application.Animals.Commands;
using ZooApp.Application.Animals.Results;

namespace ZooApp.Application.Animals;

public interface IAnimalCommandService
{
    AnimalResult CreateAnimal(CreateAnimalCommand command);
}
