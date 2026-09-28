using Domain.Models;

namespace Domain.Mappers
{
    public class EventResponseMapper :
        IMapper<Core.Entities.EventResponse, Models.EventResponse>,
        IMapper<Models.EventResponse, Core.Entities.EventResponse>
    {
        public Models.EventResponse Map(Core.Entities.EventResponse source)
        {
            var response = new Models.EventResponse();
            response.SetEventId(source.EventId);
            response.SetUserId(source.UserId);
            response.SetRespondedAt(source.RespondedAt);
            if (source.Status != Core.Enums.EventResponseStatus.Pending)
            {
                response.SetStatus(source.Status);
                response.SetRespondedAt(source.RespondedAt);
            }
            return response;
        }

        public Core.Entities.EventResponse Map(EventResponse source)
        {
            return new Core.Entities.EventResponse
            {
                EventId = source.EventId,
                UserId = source.UserId,
                Status = source.Status,
                RespondedAt = source.RespondedAt
            };
        }
    }
}