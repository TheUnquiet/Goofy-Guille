using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using REST.DTOs.Teams;

namespace REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController
        : GenericController<
            Team,
            Core.Entities.Team,
            TeamDto,
            CreateTeamDto,
            UpdateTeamDto>
    {
        private readonly ITeamService _teamService;
        private readonly IMapper<Team, TeamDto> _toDtoMapper;

        public TeamController(
            ITeamService teamService,
            IMapper<Team, TeamDto> toDtoMapper,
            IMapper<CreateTeamDto, Team> createMapper,
            IMapper<UpdateTeamDto, Team> updateMapper)
            : base(
                teamService,
                toDtoMapper,
                createMapper,
                updateMapper)
        {
            _teamService = teamService;
            _toDtoMapper = toDtoMapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeamDto>>> GetAll()
        {
            var teams = await _teamService.GetAllAsync();

            return Ok(
                teams.Select(_toDtoMapper.Map).ToList());
        }

        [HttpGet("club/{clubId:guid}")]
        public async Task<ActionResult<List<TeamDto>>> GetByClubId(Guid clubId)
        {
            var teams = await _teamService.GetByClubIdAsync(clubId);

            return Ok(
                teams.Select(_toDtoMapper.Map).ToList());
        }
    }
}