using Domain.Mappers;
using Domain.Models;
using Domain.Services.Interfaces;
using REST.DTOs.Authentication;
using REST.DTOs.Users;

namespace API.Authentication
{
    public class AuthenticationService
    {
        private readonly IExternalTokenValidator _tokenValidator;
        private readonly IUserService _userService;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IMapper<User, UserDto> _userMapper;

        public AuthenticationService(IExternalTokenValidator tokenValidator, IUserService userService, JwtTokenService jwtTokenService, IMapper<User, UserDto> userMapper)
        {
            _tokenValidator = tokenValidator;
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _userMapper = userMapper;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var identity = await _tokenValidator.ValidateAsync(request.Providor, request.IdentityToken);
            var user = await _userService.AuthenticateAsync(identity.Provider, identity.ExternalId, identity.Email, identity.DisplayName, identity.ProfileImageUrl);
            var (accessToken, expiresAt) = _jwtTokenService.CreateToken(user);
            return new LoginResponseDto
            {
                AccessToken = accessToken,
                ExpiresAt = expiresAt,
                User = _userMapper.Map(user)
            };
        }
    }
}
