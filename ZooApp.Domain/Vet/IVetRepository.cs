using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.Vet;

public interface IVetRepository
{
    Task<Vet?> GetVetById(ulong id);
    Task<Vet> Save(Vet vet);
    Task Delete(Vet vet);
    Task<List<Visit>> GetVisitsForVet(ulong vetId);
    Task<List<Visit>> GetVisitsForVisitForTheDate(ulong vetId, DateTime date);
}
