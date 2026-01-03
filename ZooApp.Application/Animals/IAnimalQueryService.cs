using ZooApp.Application.Animals.Results;

namespace ZooApp.Application.Animals;

public interface IAnimalQueryService
{
    AnimalResult getAnimalById(int id);
    List<AnimalResult> getAllAnimals();
}
