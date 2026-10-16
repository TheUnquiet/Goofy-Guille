using Domain.Exceptions;

namespace API.Authentication
{
    public class ExternalTokenValidator : IExternalTokenValidator
    {
        private readonly GoogleTokenValidator _googleTokenValidator;

        public ExternalTokenValidator(GoogleTokenValidator googleTokenValidator)
        {
            _googleTokenValidator = googleTokenValidator;
        }

        public async Task<VerifiedExternalIdentity> ValidateAsync(string provider, string identityToken)
        {
            if (string.IsNullOrWhiteSpace(provider)) throw new ValidationException("An authentication provider is required");
            if (string.IsNullOrWhiteSpace(identityToken)) throw new ArgumentException("Identity token is required", nameof(identityToken));
            if (string.Equals(provider, "Google", StringComparison.OrdinalIgnoreCase)) return await _googleTokenValidator.ValidateAsync(identityToken);
            throw new NotSupportedException($"Authentication provider '{provider}'is not supported");
        }
    }
}
