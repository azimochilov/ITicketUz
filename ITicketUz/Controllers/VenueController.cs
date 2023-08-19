using ITicketUZ.Service.DTOs.Venues;
using ITicketUZ.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITicketUZ.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VenueController : ControllerBase
{
    private readonly IVenueService venueService;
    public VenueController(IVenueService venueService)
    {
        this.venueService = venueService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> InsertAsync([FromBody] VenueCreationDto dto) =>
           Ok(await this.venueService.AddAsync(dto));

    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync() =>
        Ok(await this.venueService.RetriveAllAsync());

    

}
