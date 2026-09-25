using Core.Enums;

namespace Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public Guid ClubId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        public AuthenticationProvider? ExternalProvider { get; set; }

        public string? ExternalId { get; set; }
    }
}
