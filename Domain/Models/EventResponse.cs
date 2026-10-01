using Core.Enums;
using Domain.Exceptions;

namespace Domain.Models
{
    public class EventResponse
    {
        public Guid EventId { get; private set; }
        public Guid UserId { get; private set; }
        public EventResponseStatus Status { get; private set; }
        public DateTime? RespondedAt { get; private set; }

        public void SetEventId(Guid eventId)
        {
            if (eventId == Guid.Empty) throw new ValidationException("Event ID is required");
            EventId = eventId;
        }

        public void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty) throw new ValidationException("User ID is required");
            UserId = userId;
        }

        public void SetStatus(EventResponseStatus status)
        {
            Status = status;
            RespondedAt = DateTime.UtcNow;
        }

        public void SetRespondedAt(DateTime? respondedAt)
        {
            RespondedAt = respondedAt;
        }
    }
}