using ITicketUZ.Service.DTOs.Events;
using ITicketUZ.Service.Exceptions;
using ITicketUZ.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITicketUZ.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEvent([FromBody] EventCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _eventService.AddAsync(dto);
                return Ok(result);
            }
            catch (AppException ex)
            {
                return StatusCode(ex.Code, new { Message = ex.Message });
            }
        }

        [HttpGet("available-seats/{eventId}")]
        public async Task<IActionResult> GetAvailableSeats(long eventId)
        {
            try
            {
                var availableSeats = await _eventService.GetAvailableSeatsAsync(eventId);
                return Ok(availableSeats);
            }
            catch (AppException ex)
            {
                return StatusCode(ex.Code, new { Message = ex.Message });
            }
        }
    }

