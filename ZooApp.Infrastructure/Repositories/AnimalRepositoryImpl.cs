using Microsoft.EntityFrameworkCore;
using ZooApp.Domain.Animal;
using ZooApp.Infrastructure.Persistance;

namespace ZooApp.Infrastructure.Repositories;

public class AnimalRepositoryImpl : IAnimalRepository
{

    private readonly ZooDbContext _context;

    public AnimalRepositoryImpl(ZooDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Animal animal)
    {
        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _context.Animals.AnyAsync(s => s.Id == id);
    }

    public async Task<List<Animal>> GetAllAnimals()
    {
        return _context.Animals.ToList();
    }

    public async Task<Animal?> GetById(int id)
    {
        return _context.Animals.Find(id);
    }

    public async Task<Animal> Save(Animal animal)
    {
        if (animal.Id == 0)
        {
            _context.Animals.Add(animal);

        }
        else
        {
            _context.Animals.Update(animal);
        }
       
        await _context.SaveChangesAsync();
        return animal;
    }
}
