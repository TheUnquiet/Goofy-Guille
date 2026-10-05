using Microsoft.AspNetCore.Mvc;
using Domain.Services.Interfaces;
using REST.DTOs.Users;
using REST.Mappers;

namespace REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserMapper _mapper;

        public UserController(IUserService userService, UserMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(_mapper.Map(user));
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            return Ok(_mapper.Map(user));
        }
    }
}
