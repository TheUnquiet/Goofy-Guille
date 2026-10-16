namespace API.Authentication
{
    public interface IExternalTokenValidator
    {
        Task<VerifiedExternalIdentity> ValidateAsync(string provider, string identityToken);
    }
}
