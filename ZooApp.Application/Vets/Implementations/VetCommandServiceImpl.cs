using ZooApp.Application.Vets.Commands;
using ZooApp.Application.Vets.Results;
using ZooApp.Domain.Animal;
using ZooApp.Domain.Vets;

namespace ZooApp.Application.Vets.Implementations;

public class VetCommandServiceImpl : IVetCommandService
{

    private readonly IVetRepository _vetRepository;
    private readonly IAnimalRepository _animalRepository;

    public VetCommandServiceImpl(IVetRepository vetRepository, IAnimalRepository animalRepository)
    {
        _vetRepository = vetRepository;
        _animalRepository = animalRepository;
    }

    //public async Task<VetResult> AddVetAsync(CreateVetCommand command)
    //{
    //    var vetToBeSaved = Vet.CreateNew(
    //        command.FirstName,
    //        command.LastName,
    //        command.MonthlyHoursLimit);

    //    var savedVet = await _vetRepository.SaveAsync(vetToBeSaved);

    //    return new VetResult(
    //        savedVet.Id,
    //        savedVet.FirstName,
    //        savedVet.LastName,
    //        savedVet.MonthlyHoursLimit
    //        ); 
    //}

    public async Task ScheduleVisitAsync(int vetId, ScheduleVisitCommand command)
    {
       
        var animalExists = await _animalRepository.ExistsByIdAsync(command.AnimalId);
        if (!animalExists)
        {   
            throw new ArgumentException($"Animal with id {command.AnimalId} does not exist.");
        }

        var vet = await _vetRepository.GetVetByIdAsync(vetId);
        if (vet is null)
        {
            throw new KeyNotFoundException($"Vet with id {vetId} does not exist.");
        }

        vet.ScheduleVisit(
            command.AnimalId,
            command.ScheduledAt,
            command.DurationInHours,
            command.Description
        );

        await _vetRepository.SaveAsync(vet);
    }

    public async Task DeleteVetByIdAsync(int vetId)
    {
        var VetToBeDeleted = await _vetRepository.GetVetByIdAsync(vetId);

        if(VetToBeDeleted == null)
        {
            throw new InvalidOperationException($"Vet with id {vetId} does not exist.");
        }

        await _vetRepository.DeleteAsync(VetToBeDeleted);
    }
}
