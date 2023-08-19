namespace ITicketUZ.Service.DTOs.Events;
public class EventResultDto
{
    public string Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double Price { get; set; }
    public long VenueId { get; set; }
    public List<long> EmptySeats { get; set; }
}
