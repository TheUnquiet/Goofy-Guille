using Domain.Models;

namespace Domain.Mappers
{
    public class LocationMapper :
        IMapper<Core.Entities.Location, Models.Location>,
        IMapper<Models.Location, Core.Entities.Location>
    {
        public Models.Location Map(Core.Entities.Location source)
        {
            var location = new Models.Location();

            location.SetId(source.Id);
            location.ChangeName(source.Name);
            location.ChangeAddress(source.Street, source.StreetNumber, source.PostalCode, source.City);
            return location;
        }

        public Core.Entities.Location Map(Models.Location source)
        {
            return new Core.Entities.Location
            {
                Id = source.Id,
                Name = source.Name,
                Street = source.Street,
                StreetNumber = source.StreetNumber,
                PostalCode = source.PostalCode,
                City = source.City;
            };
        }
    }
}