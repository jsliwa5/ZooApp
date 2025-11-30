using System;
using System.Collections.Generic;
using System.Linq; 

namespace ZooApp.Domain.Vet;

public class Vet
{
    public ulong Id { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int MonthlyHoursLimit { get; private set; } 

  
    private readonly List<Visit> _visits = new();
    public IReadOnlyCollection<Visit> Visits => _visits.AsReadOnly();

    //for ORM
    protected Vet() { }

    //for creating new
    private Vet(string firstName, string lastName, int monthlyHoursLimit)
    {

        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentNullException(nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentNullException(nameof(lastName));
        if (monthlyHoursLimit <= 0) throw new ArgumentException("Limit must be positive");

        FirstName = firstName;
        LastName = lastName;
        MonthlyHoursLimit = monthlyHoursLimit;
    }

    private Vet(ulong id, string firstName, string lastName, int monthlyHoursLimit, List<Visit> visits) 
        : this(firstName, lastName, monthlyHoursLimit)
    {
        Id = id;
        _visits = visits;
    }

    public static Vet CreateNew(string firstName, string lastName, int monthlyHoursLimit)
    {
        return new Vet(firstName, lastName, monthlyHoursLimit);
    }

    public static Vet restore(ulong id, string firstName, string lastName,
        int monthlyHoursLimit, List<Visit> visits)
    {
        return new Vet(
                id,
                firstName,
                lastName,
                monthlyHoursLimit,
                visits
            );
        
    }


    public void ScheduleVisit(ulong animalId, DateTime date, int duration, string description)
    {
        
        var currentMonthlyLoad = _visits
            .Where(v => v.ScheduledAt.Year == date.Year && v.ScheduledAt.Month == date.Month)
            .Sum(v => v.DurationInHours);

        if (currentMonthlyLoad + duration > MonthlyHoursLimit)
        {
            throw new InvalidOperationException(
                $"Cannot schedule visit. Vet limit ({MonthlyHoursLimit}h) would be exceeded. " +
                $"Current load: {currentMonthlyLoad}h, New visit: {duration}h.");
        }

        var newVisit = new Visit(animalId, description, date, duration);

        _visits.Add(newVisit);
    }

    public void CancelVisit(ulong visitId)
    {
        var visit = _visits.FirstOrDefault(v => v.Id == visitId);
        if (visit == null)
        {
            throw new ArgumentException("Visit not found", nameof(visitId));
        }

        if (visit.IsCompleted)
        {
            throw new InvalidOperationException("Cannot cancel completed visit.");
        }

        _visits.Remove(visit);
    }
}