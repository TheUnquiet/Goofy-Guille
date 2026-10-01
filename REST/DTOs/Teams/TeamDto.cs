namespace REST.DTOs.Teams
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public Guid ClubId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}