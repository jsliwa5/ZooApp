using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Application.Vets;
using ZooApp.Application.Vets.Results;
using ZooApp.Domain.Vets;

namespace ZooApp.Application.Vets.Implementations;

public class VetQueryServiceImpl : IVetQueryService
{

    private readonly IVetRepository _vetRepository;

    public VetQueryServiceImpl(IVetRepository vetRepository)
    {
        _vetRepository = vetRepository;
    }

    public async Task<List<VetResult>> GetAllVetsAsync()
    {
   
        var allVets = await _vetRepository.GetAllVetsAsync();

        return allVets.Select(vet => new VetResult(
            vet.Id,
            vet.FirstName,
            vet.LastName,
            vet.MonthlyHoursLimit
            )).ToList();
    }

    public Task<VetResult> GetVetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<VisitResult>> GetVisitsForVetAsync(int vetId)
    {
        if (!await _vetRepository.ExistsByIdAsync(vetId))
        {
            throw new KeyNotFoundException($"Vet with id {vetId} was not found.");
        }

        var visits = await _vetRepository.GetVisitsForVetAsync(vetId);

        return visits.Select(visit => new VisitResult
        (
            visit.Id,
            visit.AnimalId,
            visit.Description,
            visit.ScheduledAt,
            visit.DurationInHours,
            visit.IsCompleted
        )).ToList();
    }
}
