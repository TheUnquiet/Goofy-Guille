namespace REST.DTOs.Authentication
{
    public class LoginRequestDto
    {
        public string IdentityToken { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
    }
}
