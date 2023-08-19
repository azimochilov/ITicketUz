using AutoMapper;
using ITicketUZ.Data.IRepositories;
using ITicketUZ.Data.Repositories;
using ITicketUZ.Domain.Entities;
using ITicketUZ.Service.DTOs.Events;
using ITicketUZ.Service.DTOs.Venues;
using ITicketUZ.Service.Exceptions;
using ITicketUZ.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITicketUZ.Service.Servicesl;
public class VenueService : IVenueService
{
    private readonly IRepository<Venue> repository;
    private readonly IMapper mapper;

    public VenueService(IRepository<Venue> repository, IMapper mapper)
    {
        this.mapper = mapper;
        this.repository = repository;
    }
    public async ValueTask<VenueResultDto> AddAsync(VenueCreationDto dto)
    {
        var exsist = await this.repository.SelectAsync(a => a.Name.Equals(dto.Name));

        if (exsist is not null)
        {
            throw new AppException(409, "Event already exsist!");
        }

        var mappedDto = this.mapper.Map<Venue>(dto);
        var addedDto = await this.repository.InsertAsync(mappedDto);

        await repository.SaveAsync();

        return mapper.Map<VenueResultDto>(addedDto);
    }

    public async ValueTask<IEnumerable<VenueResultDto>> RetriveAllAsync()
    {
        var venues = await this.repository.SelectAll()
            .Where(a => !a.IsDeleted)
            .ToListAsync();

        return mapper.Map<IEnumerable<VenueResultDto>>(venues);
    }
}
