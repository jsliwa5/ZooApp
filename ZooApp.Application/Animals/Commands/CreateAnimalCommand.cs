namespace ZooApp.Application.Animals.Commands;

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
