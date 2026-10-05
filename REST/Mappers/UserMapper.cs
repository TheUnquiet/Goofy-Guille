using Domain.Mappers;
using Domain.Models;
using REST.DTOs.Users;

namespace REST.Mappers
{
    public class UserMapper : IMapper<User, UserDto>, IMapper<CreateUserDto, User>, IMapper<UpdateUserDto, User>
    {
        public UserDto Map(User source)
        {
            return new UserDto
            {
                Id = source.Id,
                Email = source.Email,
                DisplayName = source.DisplayName,
                ProfileImageUrl = source.ProfileImageUrl,
                ClubId = source.ClubId,
                TeamId = source.TeamId,
                IsAdmin = source.IsAdmin
            };
        }

        public User Map(CreateUserDto source)
        {
            var user = new User();
            user.ChangeEmail(source.Email);
            user.ChangeDisplayName(source.DisplayName);
            user.ChangeProfileImage(source.ProfileImageUrl);
            if (source.ClubId.HasValue) user.AssignToClub(source.ClubId.Value);
            if (source.TeamId.HasValue) user.AssignToTeam(source.TeamId.Value);
            if (source.IsAdmin) user.PromoteToAdmin();
            return user;
        }

        public User Map(UpdateUserDto source)
        {
            var user = new User();

            user.SetId(source.Id);
            user.ChangeEmail(source.Email);
            user.ChangeDisplayName(source.DisplayName);
            user.ChangeProfileImage(source.ProfileImageUrl);
            if (source.ClubId.HasValue) user.AssignToClub(source.ClubId.Value);
            if (source.TeamId.HasValue) user.AssignToTeam(source.TeamId.Value);
            if (source.IsAdmin) user.PromoteToAdmin();
            return user;
        }
    }
}
