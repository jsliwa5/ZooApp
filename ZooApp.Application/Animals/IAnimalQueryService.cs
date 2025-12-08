using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Application.Animals.Results;

namespace ZooApp.Application.Animals;

public interface IAnimalQueryService
{
    AnimalResult getAnimalById(int id);
    List<AnimalResult> getAllAnimals();
}
