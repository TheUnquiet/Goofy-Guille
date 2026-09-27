namespace Domain.Mappers
{
    public class ClubMapper :
        IMapper<Core.Entities.Club, Models.Club>,
        IMapper<Models.Club, Core.Entities.Club>
    {
        public Models.Club Map(Core.Entities.Club source)
        {
            var club = new Models.Club();
            club.SetId(source.Id);
            club.ChangeName(source.Name);
            return club;
        }

        public Core.Entities.Club Map(Models.Club source)
        {
            return new Core.Entities.Club
            {
                Id = source.Id,
                Name = source.Name
            };
        }
    }
}
