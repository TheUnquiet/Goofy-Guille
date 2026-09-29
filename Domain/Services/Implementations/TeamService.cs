using Core.Repositories.Interfaces;
using Domain.Mappers;
using Domain.Models;
using Domain.Exceptions;
using Domain.Services.Implementations;

namespace Domain.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepo _teamRepo;
        private readonly IClubRepo _clubRepo;
        private readonly IMapper<Core.Entities.Team, Models.Team> _toDomainMapper;
        private readonly IMapper<Models.Team, Core.Entities.Team> _toEntityMapper;

        public TeamService(ITeamRepo teamRepo, IClubRepo clubRepo, IMapper<Core.Entities.Team, Models.Team> domainMapper, IMapper<Models.Team, Core.Entities.Team> entityMapper)
        {
            _teamRepo = teamRepo;
            _clubRepo = clubRepo;
            _toDomainMapper = domainMapper;
            _toEntityMapper = entityMapper;
        }

        public async Task<Team> GetByIdAsync(Guid id)
        {
            var team await _teamRepo.GetByIdAsync(id);
            if (team is null) throw new NotFoundException($"Team with ID {id} was not found");
            return _toDomainMapper.Map(team);
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
            return teams.Selet(_toDomainMapper.Map).ToList();
        }

        public async Task<Team> CreateAsync(Team team)
        {
            await ValidateClubAsync(team.ClubId);
            var entity = _toEntityMapper.Map(team);
            await _teamRepo.AddAsync(entity);
            await _teamRepo.SaveChangesAsync();
            return team;
        }

        public async Task<Team> UpdateAsync(Team team)
        {
            var existingTeam = await _teamRepo.GetByIdAsync(team.Id);
            if (existingTeam is null) throw new NotFoundException($"Team with ID {team.Id} was not found");
            await ValidateClubAsync(team.ClubId);
            existingTeam.ClubId = team.ClubId;
            existingTeam.Name = team.Name;
            existingTeam.Color = team.Color;
            existingTeam.Description = team.Description;
            await _teamRepo.UpdateAsync(existingTeam);
            await _teamRepo.SaveChangesAsync();
            return team;
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _teamRepo.GetByIdAsync(id) is null) throw new NotFoundException($"Team with ID {id} was not found");
            await _teamRepo.DeleteAsync(id);
            await _teamRepo.SaveChangesAsync();
        }

        private async Task ValidateClubAsync(Guid clubId)
        {
            if (await _clubRepo.GetByClubIdAsync(clubId) is null) throw new NotFoundException($"Club with ID {clubId} was not found");
        }
    }
}