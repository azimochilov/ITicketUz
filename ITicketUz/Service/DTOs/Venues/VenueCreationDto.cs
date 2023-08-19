namespace ITicketUZ.Service.DTOs.Venues;
public class VenueCreationDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public long Capacity { get; set; }
    public long Rows { get; set; }
    public long SeatsInRow { get; set; }
}
