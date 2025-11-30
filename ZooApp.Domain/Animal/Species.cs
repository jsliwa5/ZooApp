namespace ZooApp.Domain.Animal; 

public enum AnimalKingdom
{
    Mammal,
    Bird,
    Fish,
    Reptile,
    Amphibian
}

public record Species(
    string Name, 
    int FeedingIntervalInHours, 
    AnimalKingdom Kingdom
);