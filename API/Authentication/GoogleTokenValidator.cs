using Domain.Exceptions;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace API.Authentication
{
    public class GoogleTokenValidator
    {
        private readonly GoogleAuthenticationOptions _options;

        public GoogleTokenValidator(IOptions<GoogleAuthenticationOptions> options)
        {
            _options = options.Value;
        }

        public async Task<VerifiedExternalIdentity> ValidateAsync(string identityToken)
        {
            if (string.IsNullOrWhiteSpace(_options.ClientId)) throw new InvalidOperationException("Google authentication is not configured");
            if (string.IsNullOrWhiteSpace(identityToken)) throw new AuthenticationException("An identityToken is required");
            
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(identityToken,
                    new GoogleJsonWebSignature.ValidationSettings
                    {
                        Audience = new[] { _options.ClientId }
                    });
            } catch (InvalidJwtException ex)
            {
                throw new AuthenticationException("The Google identity token is invalid or expired");
            }
            if (string.IsNullOrWhiteSpace(payload.Subject)) throw new InvalidOperationException("Google token does not contain a subject");
            if (string.IsNullOrWhiteSpace(payload.Email) || payload.EmailVerified != true) throw new InvalidOperationException("Google account must have a verified email.");
            return new VerifiedExternalIdentity
            {
                Provider = Core.Enums.AuthenticationProvider.Google,
                ExternalId = payload.Subject,
                Email = payload.Email,
                DisplayName = payload.Name ?? payload.Email,
                ProfileImageUrl = payload.Picture
            };
        }
    }
}
