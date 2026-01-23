using ZooApp.Application.Vets.Commands;
using ZooApp.Application.Vets.Results;

namespace ZooApp.Application.Vets;

public interface IVetCommandService
{
    Task DeleteVetByIdAsync(int vetId);
    Task ScheduleVisitAsync(int vetId, ScheduleVisitCommand command);

    Task PerformVisitAsync(int vetId, int visitId);
}
