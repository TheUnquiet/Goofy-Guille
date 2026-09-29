namespace REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : GenericController
    {
        private readonly ITeamService _teamService;
        private readonly IMapper<Team, TeamDto> _toDtoMapper;
        private readonly IMapper<CreateTeamDto, Team> _createMapper;
        private readonly IMapper<UpdateTeamDto, Team> _updateMapper;

        public TeamController(ITeamService teamService, IMapper<Team, TeamDto> toDtoMapper, IMapper<CreateTeamDto, Team> createMapper, IMapper<UpdateTeamDto, Team> updateMapper)
        {
            _teamService = teamService;
            _toDtoMapper = toDtoMapper;
            _createMapper = createMapper;
            _updateMapper = updateMapper;
        }
    }
}