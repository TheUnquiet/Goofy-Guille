using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using REST.DTOs.Clubs;

namespace REST.Controllers
{
    [Route("api/[controller]")]
    public class ClubController : GenericController<Club, Core.Entities.Club, ClubDto, CreateClubDto, UpdateClubDto>
    {
        private readonly IClubService _clubService;
        private readonly IMapper<Club, ClubDto> _toDtoMapper;

        public ClubController(IClubService clubService, IMapper<Club, ClubDto> toDtoMapper,
            IMapper<CreateClubDto, Club> createMapper, IMapper<UpdateClubDto, Club> UpdateClubDto)
            : base(clubService, toDtoMapper, createMapper, updateMapper)
        {
            _clubService = clubService;
            _toDtoMapper = toDtoMapper,
        }

        [HttpGet]
        public async Task<ActionResult<List<ClubDto>>> GetAll()
        {
            var clubs = await _clubService.GetAllAsync();
            return Ok(clubs.Select(_toDtoMapper.Map).ToList());
        }
    }
}