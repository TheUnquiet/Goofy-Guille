using Domain.Mappers;
using Domain.Models;
using REST.DTOs.Events;

namespace REST.Mappers
{
    public class EventMapper : 
        IMapper<Event, EventDto>,
        IMapper<CreateEventDto, Event>,
        IMapper<UpdateEventDto, Event>
    {
        public EventDto Map(Event source)
        {
            return new EventDto
            {
                Id = source.Id,
                ClubId = source.ClubId,
                CreatedByUserId = source.CreatedByUserId,
                Type = source.Type,
                Description = source.Description,
                LocationId = source.LocationId,
                StartTime = source.StartTime,
                EndTime = source.EndTime,
                OpponentClubId = source.OpponentClubId
            };
        }

        public Event Map(CreateEventDto source)
        {
            throw new NotImplementedException();
        }

        public Event Map(UpdateEventDto source)
        {
            throw new NotImplementedException();
        }
    }
}