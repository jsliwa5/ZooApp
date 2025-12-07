using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.Vet;

public interface IVetRepository
{
    Task<Vet?> GetVetById(int id);
    Task<Vet> Save(Vet vet);
    Task Delete(Vet vet);
    Task<List<Visit>> GetVisitsForVet(int vetId);
    Task<List<Visit>> GetVisitsForVisitForTheDate(int vetId, DateTime date);
}
