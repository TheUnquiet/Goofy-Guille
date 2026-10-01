using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using Domain.Models;

namespace Domain.Services.Implementations
{
    public class TeamService : GenericService<Team, Core.Entities.Team>, ITeamService
    {
        private readonly ITeamRepo _teamRepo;
        private readonly IClubRepo _clubRepo;

        public TeamService(ITeamRepo teamRepo, IClubRepo clubRepo, IGenericRepo<Core.Entities.Team> repo,
            IMapper<Core.Entities.Team, Models.Team> domainMapper, IMapper<Models.Team, Core.Entities.Team> entityMapper)
            : base(repo, domainMapper, entityMapper)
        {
            _teamRepo = teamRepo;
            _clubRepo = clubRepo;
        }

        public async Task<List<Team>> GetByClubIdAsync(Guid clubId)
        {
            if (await _clubRepo.GetByIdAsync(clubId) is null) throw new NotFoundException($"Club with ID {clubId} was not found");
            var teams = await _teamRepo.GetByClubIdAsync(clubId);
            return teams.Select(_toDomainMapper.Map).ToList();
        }

        public async Task<List<Team>> GetAllAsync()
        {
            var teams = await _teamRepo.GetAllAsync();
            return teams.Select(_toDomainMapper.Map).ToList();
        }

        public override async Task<Team> CreateAsync(Team team)
        {
            await ValidateClubAsync(team.ClubId);
            return await base.CreateAsync(team);
        }

        public override async Task<Team> UpdateAsync(Team team)
        {
            await ValidateClubAsync(team.ClubId);
            return await base.UpdateAsync(team);
        }

        private async Task ValidateClubAsync(Guid clubId)
        {
            if (await _clubRepo.GetByIdAsync(clubId) is null) throw new NotFoundException($"Club with ID {clubId} was not found");
        }
    }
}