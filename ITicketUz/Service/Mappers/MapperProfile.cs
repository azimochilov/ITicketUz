using AutoMapper;
using ITicketUZ.Domain.Entities;
using ITicketUZ.Service.DTOs.Events;
using ITicketUZ.Service.DTOs.Tickets;
using ITicketUZ.Service.DTOs.Venues;

namespace ITicketUZ.Service.Mappers;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Venue, VenueCreationDto>().ReverseMap();
        CreateMap<Venue, VenueResultDto>().ReverseMap();
        CreateMap<VenueCreationDto, VenueResultDto>().ReverseMap();

        CreateMap<Ticket, TicketCreationDto>().ReverseMap();
        CreateMap<Ticket, TicketResultDto>().ReverseMap();
        CreateMap<TicketCreationDto, TicketResultDto>().ReverseMap();

        CreateMap<Event, EventCreationDto>().ReverseMap();
        CreateMap<Event, EventResultDto>().ReverseMap();
        CreateMap<EventCreationDto, EventResultDto>().ReverseMap();
    }
}
