using Domain.Mappers;
using Domain.Models;
using REST.DTOs.Teams;

namespace REST.Mappers
{
    public class TeamMapper :
        IMapper<Team, TeamDto>,
        IMapper<CreateTeamDto, Team>,
        IMapper<UpdateTeamDto, Team>
    {
        public TeamDto Map(Team source)
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

        public Team Map(CreateTeamDto source)
        {
            var team = new Team();

            team.SetClub(source.ClubId);
            team.ChangeName(source.Name);
            team.ChangeColor(source.Color);
            team.ChangeDescription(source.Description);

            return team;
        }

        public Team Map(UpdateTeamDto source)
        {
            var team = new Team();

            team.SetId(source.Id);
            team.SetClub(source.ClubId);
            team.ChangeName(source.Name);
            team.ChangeColor(source.Color);
            team.ChangeDescription(source.Description);

            return team;
        }
    }
}