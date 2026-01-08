using ZooApp.Application.Vets.Commands;
using ZooApp.Application.Vets.Results;

namespace ZooApp.Application.Vets;

public interface IVetCommandService
{
    Task<VetResult> AddVetAsync(CreateVetCommand command);
    Task DeleteVetByIdAsync(int vetId);
    Task ScheduleVisitAsync(int vetId, ScheduleVisitCommand command);
}
