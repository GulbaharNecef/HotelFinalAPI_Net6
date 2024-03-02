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
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDTO model)
        {
            var result = await _userService.CreateUser(model);
            return StatusCode(result.StatusCode, result);
        }
    }
}
