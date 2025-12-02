using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Domain.Report;

namespace ZooApp.Domain.Services;

public interface IAnimalHealthReportService
{
    Task<IEnumerable<NeglectedAnimalInfo>> GetNeglectedAnimalsAsync(DateTime? sinceDate);
}
