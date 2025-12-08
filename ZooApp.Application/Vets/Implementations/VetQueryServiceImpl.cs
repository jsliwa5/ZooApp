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
}
