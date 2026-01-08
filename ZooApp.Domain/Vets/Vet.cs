namespace ZooApp.Domain.Vets;

public class Vet
{
    public int Id { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int MonthlyHoursLimit { get; private set; } 

  
    private readonly List<Visit> _visits = new();
    public IReadOnlyCollection<Visit> Visits => _visits.AsReadOnly();

    public Guid UserId { get; private set; }

    //for ORM
    protected Vet() { }

    //for creating new
    private Vet(string firstName, string lastName, int monthlyHoursLimit, Guid userId)
    {

        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentNullException(nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentNullException(nameof(lastName));
        if (monthlyHoursLimit <= 0) throw new ArgumentException("Limit must be positive");

        FirstName = firstName;
        LastName = lastName;
        MonthlyHoursLimit = monthlyHoursLimit;
        UserId = userId;
    }

    //for restoring
    private Vet(int id, string firstName, string lastName, int monthlyHoursLimit, List<Visit> visits, Guid userId) 
        : this(firstName, lastName, monthlyHoursLimit, userId)
    {
        Id = id;
        _visits = visits;
    }

    public static Vet CreateNew(string firstName, string lastName, int monthlyHoursLimit, Guid userId)
    {
        return new Vet(firstName, lastName, monthlyHoursLimit, userId);
        
    }

    public static Vet restore(int id, string firstName, string lastName,
        int monthlyHoursLimit, List<Visit> visits, Guid userId)
    {
        return new Vet(
                id,
                firstName,
                lastName,
                monthlyHoursLimit,
                visits,
                userId
            );
        
    }


    public void ScheduleVisit(int animalId, DateTime scheduledAt, int durationInHours, string description)
    {
        
        var currentMonthlyLoad = _visits
            .Where(v => v.ScheduledAt.Year == scheduledAt.Year && v.ScheduledAt.Month == scheduledAt.Month)
            .Sum(v => v.DurationInHours);

        if (currentMonthlyLoad + durationInHours > MonthlyHoursLimit)
        {
            throw new InvalidOperationException(
                $"Cannot schedule visit. Vet limit ({MonthlyHoursLimit}h) would be exceeded. " +
                $"Current load: {currentMonthlyLoad}h, New visit: {durationInHours}h.");
        }

        var newVisit = new Visit(this.Id, animalId, description, scheduledAt, durationInHours);

        _visits.Add(newVisit);
    }

    public void CancelVisit(int visitId)
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