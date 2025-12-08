using System;
using System.Collections.Generic;
using System.Text;

namespace ZooApp.Application.Vets.Commands;

public record CreateVetCommand
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int MonthlyHoursLimit { get; init; }

    public CreateVetCommand(string firstName, string lastName, int monthlyHoursLimit)
    {
        FirstName = firstName;
        LastName = lastName;
        MonthlyHoursLimit = monthlyHoursLimit;
    }
}
