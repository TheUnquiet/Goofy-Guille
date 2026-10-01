using Core.Enums;

namespace Core.Entities
{
    public class Event
    {
        public Guid Id { get; set; }

        public Guid ClubId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public EventType Type { get; set; }

        public string? Description { get; set; }

        public Guid LocationId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid? OpponentClubId { get; set; }
    }
}
