using ITicketUZ.Domain.Entities;
using ITicketUZ.Service.DTOs.Events;

namespace ITicketUZ.Service.Interfaces;
public interface IEventService
{
    ValueTask<EventResultDto> AddAsync(EventCreationDto dto);    
    ValueTask<bool> CheckEventConflicts(Event newEvent);
    ValueTask<List<long>> GetAvailableSeatsAsync(long eventId);
    ValueTask<IEnumerable<long>> GetAllSeats(Venue venue);
}
