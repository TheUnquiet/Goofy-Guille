using Core.Enums;

namespace Core.Entities
{
    public class EventResponse
    {
        public Guid EventId { get; set; }

        public Guid UserId { get; set; }

        public EventResponseStatus Status { get; set; }

        public DateTime? RespondedAt { get; set; }
    }
}
