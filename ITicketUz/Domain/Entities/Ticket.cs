using ITicketUZ.Domain.Commons;

namespace ITicketUZ.Domain.Entities;
public class Ticket : Auditable
{    
    public string FullName { get; set; }
    public long VenueId { get; set; }
    public long Row { get; set; }
    public long Seat { get; set; }
    public long EventId{ get; set; }
    public string Secret { get; set; }
}
