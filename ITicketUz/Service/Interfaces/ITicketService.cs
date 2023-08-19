using ITicketUZ.Domain.Entities;
using ITicketUZ.Service.DTOs.Tickets;
using ITicketUZ.Service.DTOs.Venues;

namespace ITicketUZ.Service.Interfaces;
public interface ITicketService
{
    ValueTask<bool> CancelTicketAsync(long ticketId, string secret);
    ValueTask<TicketResultDto> BuyTicketAsync(TicketCreationDto dto);
}
