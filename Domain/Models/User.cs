using Core.Enums;
using Domain.Exceptions;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; } = string.Empty;
        public string DisplayName { get; private set; } = string.Empty;
        public string? ProfileImageUrl { get; private set; }
        public Guid? ClubId { get; private set; }
        public Guid? TeamId { get; private set; }
        public bool IsAdmin { get; private set; }
        public AuthenticationProvider? ExternalProvider { get; private set; }
        public string? ExternalId { get; private set; }

        public void SetId(Guid id)
        {
            if (id == Guid.Empty) throw new ValidationException("User ID is required.");
            Id = id;
        }

        public void ChangeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ValidationException("Email is required.");
            Email = email;
        }

        public void ChangeDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName)) throw new ValidationException("Display name is required.");
            DisplayName = displayName;
        }

        public void AssignToClub(Guid clubId)
        {
            if (clubId == Guid.Empty) throw new ValidationException("Club ID is required.");
            ClubId = clubId;
        }

        public void RemoveFromClub()
        {
            ClubId = null;
            TeamId = null;
        }

        public void PromoteToAdmin()
        {
            IsAdmin = true;
        }

        public void DemoteFromAdmin()
        {
            IsAdmin = false;
        }

        public void AssignToTeam(Guid teamId)
        {
            if (teamId == Guid.Empty) throw new ValidationException("Team ID is required");
            TeamId = teamId;

        }

        public void RemoveFromTeam()
        {
            TeamId = null;
        }

        public void ChangeProfileImage(string? profileImageUrl)
        {
            ProfileImageUrl = profileImageUrl;
        }

        public void SetExternalLogin(AuthenticationProvider provider, string externalId)
        {
            if (string.IsNullOrWhiteSpace(externalId)) throw new ValidationException("External ID is required.");
            ExternalProvider = provider;
            ExternalId = externalId;
        }
    }
}

