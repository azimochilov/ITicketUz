using ITicketUZ.Service.DTOs.Tickets;
using ITicketUZ.Service.Exceptions;
using ITicketUZ.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITicketUZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyTicket([FromBody] TicketCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _ticketService.BuyTicketAsync(dto);
                return Ok(result);
            }
            catch (AppException ex)
            {
                return StatusCode(ex.Code, new { Message = ex.Message });
            }
        }

        [HttpDelete("{ticketId}")]
        public async Task<IActionResult> CancelTicket(long ticketId, [FromQuery] string secret)
        {
            try
            {
                var success = await _ticketService.CancelTicketAsync(ticketId, secret);
                if (success)
                {
                    return Ok(new { Message = "Ticket canceled successfully." });
                }
                return NotFound(new { Message = "Ticket not found or invalid secret." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while canceling the ticket." });
            }
        }
    }

}
