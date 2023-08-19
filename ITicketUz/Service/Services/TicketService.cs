using AutoMapper;
using ITicketUZ.Data.IRepositories;
using ITicketUZ.Domain.Entities;
using ITicketUZ.Service.DTOs.Events;
using ITicketUZ.Service.DTOs.Tickets;
using ITicketUZ.Service.Exceptions;
using ITicketUZ.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ITicketUZ.Service.Services;
public class TicketService : ITicketService
{
    private readonly IRepository<Ticket> repository;
    private readonly IRepository<Event> eRepository;
    private readonly IRepository<Venue> vRepository;
    private readonly IEventService eventService;
    private readonly IMapper mapper;

    public TicketService(IRepository<Ticket> repository,IRepository<Event> eRepository, IRepository<Venue> vRepository, IEventService eventService, IMapper mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.eRepository = eRepository;
        this.vRepository = vRepository;
        this.eventService = eventService;
    }

    public async ValueTask<bool> CancelTicketAsync(long ticketId, string secret)
    {
        var ticket = await this.repository.SelectAsync(t => t.Id == ticketId);
        if (ticket != null && ticket.Secret == secret)
        {
            await this.repository.DeleteAsync(t => t.Id == ticketId);
            await this.repository.SaveAsync();
            return true;
        }
        return false;
    }


    public async ValueTask<TicketResultDto> BuyTicketAsync(TicketCreationDto dto)
    {
        var existingEvent = await eRepository.SelectAsync(e => e.Id == dto.EventId);

        if (existingEvent is null)
        {
            throw new AppException(404, "Event not found");
        }

        var venue = await vRepository.SelectAsync(v => v.Id == existingEvent.VenueId);

        if (dto.Row < 1 || dto.Row > venue.Rows || dto.Seat < 1 || dto.Seat > venue.SeatsInRow)
        {
            throw new AppException(400, "Invalid row or seat number");
        }

        var availableSeats = await eventService.GetAvailableSeatsAsync(dto.EventId);

        var selectedSeat = Tuple.Create(dto.Row, dto.Seat);
        

        existingEvent.EmptySeats ??= new List<long>();
        existingEvent.EmptySeats.Remove(selectedSeat.Item2 + (selectedSeat.Item1 - 1) * venue.SeatsInRow);
        

        var addedTicket = await repository.InsertAsync(this.mapper.Map<Ticket>(dto));

        await eRepository.SaveAsync();
        await repository.SaveAsync();

        return mapper.Map<TicketResultDto>(addedTicket);
    }

}
