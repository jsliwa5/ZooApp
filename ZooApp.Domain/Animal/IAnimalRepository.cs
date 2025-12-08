using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.Animal;

public interface IAnimalRepository
{
    Task<Animal?> GetById(int id);
    Task<Animal> Save(Animal animal);
    Task Delete(Animal animal);
    Task<List<Animal>> GetAllAnimals();
    Task<bool> ExistsByIdAsync(int id);
}
