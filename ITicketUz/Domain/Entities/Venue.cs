using ITicketUZ.Domain.Commons;

namespace ITicketUZ.Domain.Entities;
public class Venue : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public long Capacity { get; set; }
    public long Rows { get; set; }
    public long SeatsInRow { get; set; }
}
