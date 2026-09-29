namespace REST.DTOs.Teams
{
    // Same fields, ID comes from URL: PUT /api/teams/{id}
    public class UpdateTeamDto
    {
        public Guid Id { get; set; }
        public Guid ClubId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}