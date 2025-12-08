using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Application.Vets.Results;

//without visits
public record VetResult
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int MonthlyHoursLimit { get; init; }

    public VetResult(int id, string firstName, string lastName, int monthlyHoursLimit)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MonthlyHoursLimit = monthlyHoursLimit;
    }

}
