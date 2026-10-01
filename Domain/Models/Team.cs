using Domain.Exceptions;

namespace Domain.Models
{
    public class Team
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ClubId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public void SetId(Guid id)
        {
            if (id == Guid.Empty) throw new ValidationException("Team ID is required");
            Id = id;
        }

        public void SetClub(Guid clubId)
        {
            if (clubId == Guid.Empty)  throw new ValidationException("Club ID is required");
            ClubId = clubId;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ValidationException("Name is required");
            Name = name;
        }

        public void ChangeDescription(string? description)
        {
            Description = description;
        }

        public void ChangeColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color)) throw new ValidationException("Team color is required");
            Color = color;
        }
    }
}