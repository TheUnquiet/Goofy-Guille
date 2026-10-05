namespace Domain.Mappers
{
    public class UserMapper : 
        IMapper<Core.Entities.User, Domain.Models.User>,
        IMapper<Domain.Models.User, Core.Entities.User>
    {
        public Models.User Map(Core.Entities.User source)
        {
            var user = new Domain.Models.User();
            user.SetId(source.Id);
            user.ChangeEmail(source.Email);
            user.ChangeDisplayName(source.DisplayName);
            user.ChangeProfileImage(source.ProfileImageUrl);
            if (source.ExternalProvider.HasValue && !string.IsNullOrWhiteSpace(source.ExternalId))
            {
                user.SetExternalLogin(
                    source.ExternalProvider.Value,
                    source.ExternalId);
            }
            if (source.ClubId.HasValue) user.AssignToClub(source.ClubId.Value);
            if (source.TeamId.HasValue) user.AssignToTeam(source.TeamId.Value);
            if (source.IsAdmin) user.PromoteToAdmin();
            return user;
        }

        public Core.Entities.User Map(Models.User source)
        {
            return new Core.Entities.User
            {
                Id = source.Id,
                Email = source.Email,
                DisplayName = source.DisplayName,
                ProfileImageUrl = source.ProfileImageUrl,
                ClubId = source.ClubId,
                TeamId = source.TeamId,
                IsAdmin = source.IsAdmin,
                ExternalProvider = source.ExternalProvider,
                ExternalId = source.ExternalId,
            };

        }
    }
}
