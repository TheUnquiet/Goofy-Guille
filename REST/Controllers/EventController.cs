using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REST.DTOs.Events;

namespace REST.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : GenericController<Event, Core.Entities.Event, EventDto, CreateEventDto, UpdateEventDto>
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly IMapper<Event, EventDto> _toDtoMapper;

        public EventController(IEventService eventService, IUserService userService, IMapper<Event, EventDto> toDtoMapper, IMapper<CreateEventDto, Event> createMapper, IMapper<UpdateEventDto, Event> updateMapper) : base(eventService, toDtoMapper, createMapper, updateMapper)
        {
            _eventService = eventService;
            _userService = userService;
            _toDtoMapper = toDtoMapper;
        }

        protected override void OnCreating(Event @event)
        {
            @event.SetCreatedByUser(GetCurrentUserId());
        }

        [HttpGet("club/{clubId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<EventDto>>> GetByClubId(Guid clubId)
        {
            var events = await _eventService.GetByClubIdAsync(clubId);
            return Ok(events.Select(_toDtoMapper.Map).ToList());
        }

        [HttpGet("my-club")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<List<EventDto>>> GetMyClubEvents()
        {
            var userId = GetCurrentUserId();
            var user = await _userService.GetByIdAsync(userId) ?? throw new Domain.Exceptions.NotFoundException("User not found.");
            if (user.ClubId == null) return Ok(new List<EventDto>());
            var events = await _eventService.GetByClubIdAsync(user.ClubId.Value);
            return Ok(events.Select(_toDtoMapper.Map).ToList());
        }
    }
}
