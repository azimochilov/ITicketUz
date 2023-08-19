using AutoMapper;
using ITicketUZ.Data.IRepositories;
using ITicketUZ.Domain.Entities;
using ITicketUZ.Service.DTOs.Events;
using ITicketUZ.Service.Exceptions;
using ITicketUZ.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITicketUZ.Service.Services;
public class EventService : IEventService
{
    private readonly IRepository<Event> repository;
    private readonly IRepository<Venue> vRepository;
    private readonly IMapper mapper;

    public EventService(IRepository<Event> repository, IMapper mapper, IRepository<Venue> vRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.vRepository = vRepository;
    }

    public async ValueTask<EventResultDto> AddAsync(EventCreationDto dto)
    {
        var existingEvent = await this.repository.SelectAsync(a => a.Name.Equals(dto.Name));

        if (existingEvent is not null)
        {
            throw new AppException(409, "Event already exists!");
        }

        var mappedDto = this.mapper.Map<Event>(dto);

        if (await CheckEventConflicts(mappedDto))
        {
            throw new AppException(409, "Event conflicts with existing events!");
        }

        var addedDto = await this.repository.InsertAsync(mappedDto);

        await repository.SaveAsync();

        return mapper.Map<EventResultDto>(addedDto);
    }

    public async ValueTask<bool> CheckEventConflicts(Event newEvent)
    {
        var events = this.repository.SelectAll()
            .Where(a => !a.IsDeleted)
            .ToList();

        foreach (var existingEvent in events)
        {
            if (newEvent.Start < existingEvent.End.AddHours(1) && newEvent.End > existingEvent.Start.AddHours(-1))
            {
                return true; 
            }
        }
        return false; 
    }

    public async ValueTask<List<long>> GetAvailableSeatsAsync(long eventId)
    {
        var existingEvent = await this.repository.SelectAsync(e => e.Id == eventId);

        if (existingEvent is null)
        {
            throw new AppException(404, "Event not found");
        }

        var venue = await vRepository.SelectAsync(v => v.Id == existingEvent.VenueId);

        var allSeats = await GetAllSeats(venue);
        var bookedSeats = existingEvent.EmptySeats ?? new List<long>();

        var availableSeats = allSeats.Except(bookedSeats).ToList();

        return availableSeats;
    }

    public async ValueTask<IEnumerable<long>> GetAllSeats(Venue venue)
    {
        var totalSeats = (int)(venue.Rows * venue.SeatsInRow);
        return Enumerable.Range(1, totalSeats).Select(s => (long)s);
    }
}
