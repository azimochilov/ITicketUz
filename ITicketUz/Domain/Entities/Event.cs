using ITicketUZ.Domain.Commons;

namespace ITicketUZ.Domain.Entities;
public class Event : Auditable
{
    public string Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double Price { get; set; }
    public long VenueId { get; set; }
    public List<long> EmptySeats { get; set; }
}
