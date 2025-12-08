using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.Vets;

public interface IVetRepository
{
    Task<Vet?> GetVetByIdAsync(int id);
    Task<List<Vet>> GetAllVetsAsync();
    Task<Vet> SaveAsync(Vet vet);
    Task DeleteAsync(Vet vet);
    Task<List<Visit>> GetVisitsForVet(int vetId);
    Task<List<Visit>> GetVisitsForVisitForTheDate(int vetId, DateTime date);

    Task<bool> ExistsByIdAsync(int id);
}
