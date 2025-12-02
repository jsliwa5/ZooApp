using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Animal;
using ZooApp.Infrastructure.Persistence;

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

    public async Task<List<Animal>> GetAllAnimals()
    {
        return _context.Animals.ToList();
    }

    public async Task<Animal?> GetById(ulong id)
    {
        return _context.Animals.Find(id);
    }

    public async Task<Animal> Save(Animal animal)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();
        return animal;
    }
}
