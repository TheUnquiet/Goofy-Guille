using Core.Enums;

namespace REST.DTOs.Events
{
    public class CreateEventDTo
    {
        public Guid ClubId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public EventType Type { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? OpponentClubId { get; set; }
    }
}