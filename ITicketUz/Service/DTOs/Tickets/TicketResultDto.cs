namespace ITicketUZ.Service.DTOs.Tickets;
public class TicketResultDto
{
    public string FullName { get; set; }
    public long VenueId { get; set; }
    public long Row { get; set; }
    public long Seat { get; set; }
    public long EventId { get; set; }
    public string Secret { get; set; }
}
