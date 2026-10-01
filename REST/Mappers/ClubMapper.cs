using Domain.Mappers;
using Domain.Models;
using REST.DTOs.Clubs;

namespace REST.Mappers
{
    public class ClubMapper : IMapper<Club, ClubDto>, IMapper<CreateClubDto, Club>, IMapper<UpdateClubDto, Club>
    {
        public ClubDto Map(Club source)
        {
            return new ClubDto
            {
                Id = source.Id,
                Name = source.Name
            };
        }

        public Club Map(CreateClubDto source)
        {
            var club = new Club();
            club.ChangeName(source.Name);
            return club;
        }

        public Club Map(UpdateClubDto source)
        {
            var club = new Club();
            club.SetId(source.Id);
            club.ChangeName(source.Name);
            return club;
        }
    }
}