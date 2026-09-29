using Domain.Models;

namespace Domain.Mappers
{
    public class TeamMapper :
        IMapper<Core.Entities.Team, Models.Team>,
        IMapper<Models.Team, Core.Entities.Team>
    {
        public Models.Team Map(Core.Entities.Team source)
        {
            var team = new Team();
            team.SetId(source.Id);
            team.SetClub(source.ClubId);
            team.ChangeName(source.Name);
            team.ChangeDescription(source.Description);
            return team;
        }

        public Core.Entities.Team Map(Team source)
        {
            return new Core.Entities.Team
            {
                Id = source.Id;
                ClubId = source.clubId;
                Name = source.Name,
                Color = source.Color,
                Description = source.Description
            };
        }
    }
}