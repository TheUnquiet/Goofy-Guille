namespace Core.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public Guid ClubId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Color { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}