using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.Animal;

public interface IAnimalRepository
{
    Task<Animal?> GetByIdAsync(int id);
    Task<Animal> SaveAsync(Animal animal);
    Task DeleteAsync(Animal animal);
    Task<List<Animal>> GetAllAnimalsAsync();
    Task<bool> ExistsByIdAsync(int id);
}
