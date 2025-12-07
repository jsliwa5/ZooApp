using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Domain.Report;

public record NeglectedAnimalInfo(
    int AnimalId,
    string AnimalName,
    string SpeciesName,
    DateTime LastFedAt,
    int DaysSinceLastFed,
    string SeverityLevel
    )
{
}
