using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRolesToUser(string userIdOrName)
        {
            var result = await _userService.GetRolesToUserAsync(userIdOrName);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserCreateDTO model)
        {
            var result = await _userService.Register(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AssignUserToRoles(string userId, string[] roles)
        {
            var data = await _userService.AssignUserToRoleAsync(userId, roles);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteUser(string userIdOrName)
        {
            var data = await _userService.DeleteUserAsync(userIdOrName);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO model)
        {
            var data = await _userService.UpdateUserAsync(model);
            return StatusCode(data.StatusCode, data);
        }

    }
}
