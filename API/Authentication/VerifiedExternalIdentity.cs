using Core.Enums;

namespace API.Authentication
{
    public class VerifiedExternalIdentity
    {
        public AuthenticationProvider Provider { get; set; }
        public string ExternalId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
    }
}
