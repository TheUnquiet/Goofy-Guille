using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Authentication;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Authentication
{
    public class JwtTokenService
    {
        private readonly JwtAuthenticationOptions _options;

        public JwtTokenService(IOptions<JwtAuthenticationOptions> options)
        {
            _options = options.Value;
        }

        public (string Accesstoken, DateTime ExpiresAt) CreateToken(User user)
        {
            if (string.IsNullOrWhiteSpace(_options.SecretKey)) throw new InvalidOperationException("JWT signing key is not configured");
            var expiresAt = DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(ClaimTypes.Name, user.DisplayName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.IsAdmin ? "Admin" : "Member")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _options.Issuer, audience: _options.Audience, claims: claims, expires: expiresAt, signingCredentials: credentials);
            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }
    }
}
