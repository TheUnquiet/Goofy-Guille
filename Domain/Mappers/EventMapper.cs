using Domain.Models;

namespace Domain.Mappers
{
    public class EventMapper :
        IMapper<Core.Entities.Event, Models.Event>,
        IMapper<Models.Event, Core.Entities.Event>
    {
        public Models.Event Map(Core.Entities.Event source)
        {
            var @event = new Event();
            @event.SetId(source.Id);
            @event.SetClub(source.ClubId);
            @event.SetCreatedByUser(source.CreatedByUserId);
            @event.SetType(source.Type);
            @event.ChangeDescription(source.Description);
            @event.SetLocation(source.LocationId);
            @event.SetTime(source.StartTime, source.EndTime);
            if (source.OpponentClubId.HasValue) @event.SetOpponentClub(source.OpponentClubId.Value);
            return @event;
        }

        public Core.Entities.Event Map(Models.Event source)
        {
            return new Core.Entities.Event
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
    }
}