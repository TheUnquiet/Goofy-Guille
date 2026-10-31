using Core.Enums;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventResponseController : ControllerBase
    {
        private readonly IEventResponseService _service;

        public EventResponseController(IEventResponseService service)
        {
            _service = service;
        }

        [HttpPost("{eventId:guid}")]
        public async Task<IActionResult> Respond(Guid eventId, [FromBody] EventResponseStatus status)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId)) throw new Domain.Exceptions.UnauthorizedException("User ID is missing or invalid");
            await _service.RespondAsync(eventId, userId, status);
            return NoContent();
        }
    }
}
