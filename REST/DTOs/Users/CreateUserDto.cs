namespace REST.DTOs.Users
{
    public class CreateUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public Guid? ClubId { get; set; }
        public Guid? TeamId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
