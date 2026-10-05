using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using REST.DTOs.Events;

namespace REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : GenericController<Event, Core.Entities.Event, EventDto, CreateEventDto, UpdateEventDto>
    {
        private readonly IEventService _eventService;
        private readonly IMapper<Event, EventDto> _toDtoMapper;

        public EventController(IEventService eventService, IMapper<Event, EventDto> toDtoMapper, IMapper<CreateEventDto, Event> createMapper, IMapper<UpdateEventDto, Event> updateMapper) : base(eventService, toDtoMapper, createMapper, updateMapper)
        {
            _eventService = eventService;
            _toDtoMapper = toDtoMapper;
        }

        [HttpGet("club/{clubId:guid}")]
        public async Task<ActionResult<List<EventDto>>> GetByClubId(Guid clubId)
        {
            var events = await _eventService.GetByClubIdAsync(clubId);
            return Ok(events.Select(_toDtoMapper.Map).ToList());
        }
    }
}
