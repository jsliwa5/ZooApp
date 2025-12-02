using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.Animal;

public interface IAnimalRepository
{
    Task<Animal?> GetById(ulong id);
    Task<Animal> Save(Animal animal);
    Task Delete(Animal animal);
    Task<List<Animal>> GetAllAnimals();
}
