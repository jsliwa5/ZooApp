using ZooApp.Application.Vets.Results;

namespace ZooApp.Application.Vets;

public interface IVetQueryService
{
    Task<List<VetResult>> GetAllVetsAsync();
    Task<VetResult> GetVetByIdAsync(int id);

    Task<List<VisitResult>> GetVisitsForVetAsync(int vetId);
}
