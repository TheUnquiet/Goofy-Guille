using Domain.Models;
using REST.DTOs.Teams;

namespace REST.Mappers
{
    public class TeamMapper : IMapper<Models.Team, TeamDto>, IMapper<CreateTeamDto, Models.Team>, IMapper<UpdateTeamDto, Models.Team>
    {
        public TeamDto Map(Models.Team source)
        {
            return new TeamDto
            {
                Id = source.Id,
                ClubId = source.ClubId,
                Name = source.Name,
                Color = source.Color,
                Description = source.Description
            };
        }

        public Models.Team Map(CreateTeamDTo source)
        {
            var team = new Team();
            team.SetClub(source.ClubId);
            team.ChangeName(source.Name);
            team.ChangeColor(source.Color);
            team.ChangeDescription(source.Description);
            return team;
        }

        public Models.Team Map(UpdateTeamDto source)
        {
            var team = new Models.Team();
            team.SetId(source.Id);
            team.SetClub(source.ClubId);
            team.SetClub(source.Name);
            team.ChangeColor(source.Color);
            team.ChangeDescription(source.Description);
            return team;
        }
    }
}