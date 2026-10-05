using Core.Enums;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Event
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ClubId { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public EventType Type { get; private set; }
        public string? Description { get; private set; }
        public Guid LocationId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public Guid? OpponentClubId { get; private set; }

        public void SetId(Guid id)
        {
            if (id == Guid.Empty) throw new ValidationException("Event ID is required");
            Id = id;
        }

        public void SetClub(Guid clubId)
        {
            if (clubId == Guid.Empty) throw new ValidationException("Club ID is required");
            if (clubId == ClubId) return;
            ClubId = clubId;
        }

        public void SetCreatedByUser(Guid userId)
        {
            if (userId == Guid.Empty) throw new ValidationException("Creator user ID is required");
            if (userId == CreatedByUserId) return;
            CreatedByUserId = userId;
        }

        public void SetType(EventType type)
        {
            if (type == Type) return;
            Type = type;
        }

        public void ChangeDescription(string? description)
        {
            Description = description;
        }

        public void SetLocation(Guid locationId)
        {
            if (locationId == Guid.Empty) throw new ValidationException("Location ID is required");
            LocationId = locationId;
        }

        public void SetTime(DateTime startTime, DateTime endTime)
        {
            if (endTime <= startTime) throw new ValidationException("End time must be after start time");
            StartTime = startTime;
            EndTime = endTime;
        }

        public void SetOpponentClub(Guid? opponentClubId)
        {
            if (opponentClubId.HasValue && opponentClubId == Guid.Empty) throw new ValidationException("Opponent club ID is invalid");
            OpponentClubId = opponentClubId;
        }
    }
}