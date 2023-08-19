using ITicketUZ.Service.DTOs.Venues;

namespace ITicketUZ.Service.Interfaces;
public interface IVenueService
{
    ValueTask<VenueResultDto> AddAsync(VenueCreationDto dto);
    ValueTask<IEnumerable<VenueResultDto>> RetriveAllAsync();

}
