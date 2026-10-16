using Microsoft.AspNetCore.Mvc;
using REST.DTOs.Authentication;

namespace API.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginRequestDto>> Login(LoginRequestDto request)
        {
            var response = await _authenticationService.LoginAsync(request);
            return Ok(response);
        }
    }
}
