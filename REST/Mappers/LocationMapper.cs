using Domain.Mappers;
using Domain.Models;
using REST.DTOs.Locations;

namespace REST.Mappers
{
    public class LocationMapper :
        IMapper<Location, LocationDto>,
        IMapper<CreateLocationDto, Location>,
        IMapper<UpdateLocationDto, Location>
    {
        public LocationDto Map(Location source)
        {
            return new LocationDto
            {
                Id = source.Id,
                Name = source.Name,
                Street = source.Street,
                StreetNumber = source.StreetNumber,
                PostalCode = source.PostalCode,
                City = source.City,
                Latitude = source.Latitude,
                Longitude = source.Longitude,
            };
        }

        public Location Map(CreateLocationDto source)
        {
            var location = new Location();
            location.ChangeName(source.Name);
            location.ChangeAddress(source.Street, source.StreetNumber, source.PostalCode, source.City);
            location.SetCoordinates(source.Latitude, source.Longitude);
            return location;
        }

        public Location Map(UpdateLocationDto source)
        {
            var location = new Location();
            location.SetId(source.Id);
            location.ChangeName(source.Name);
            location.ChangeAddress(source.Street, source.StreetNumber, source.PostalCode, source.City);
            location.SetCoordinates(source.Latitude, source.Longitude);
            return location;
        }
    }
}