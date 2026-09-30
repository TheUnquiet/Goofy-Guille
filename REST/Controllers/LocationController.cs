using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using REST.DTOs.Locations;

namespace REST.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : GenericController<Location, Core.Entities.Location, 
        LocationDto, CreateLocationDto, UpdateLocationDto>
    {
        private readonly ILocationService _locationService;
        private readonly IMapper<Location, LocationDto> _toDtoMapper;

        public LocationController(ILocationService locationService, IMapper<Location, LocationDto> toDtoMapper, 
            IMapper<CreateLocationDto, Location> createMapper, IMapper<UpdateLocationDto, Location> updateMapper)
        {
            _locationService = locationService;
            _toDtoMapper = toDtoMapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<LocationDto>>> GetAll()
        {
            var locations = await _locationService.GetAllAsync();
            return Ok(locations.Select(_toDtoMapper.Map).ToList());
        }
    }
}