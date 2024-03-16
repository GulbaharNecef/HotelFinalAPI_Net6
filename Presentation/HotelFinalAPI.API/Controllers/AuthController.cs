using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromQuery] string usernameOrEmail, [FromQuery] string password /* ,int accessTokenLifeTime*/)
        {
            var data = await _authService.Login(usernameOrEmail, password/*, 1*/);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithRefreshToken([FromBody] string refreshToken)
        {
            var data = await _authService.RefreshTokenLoginAsync(refreshToken);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout(string usernameOrEmail)
        {
            var data = await _authService.Logout(usernameOrEmail);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ResetPassword(string email, string currentPass, string newPass)
        {
            var data = await _authService.ResetPasswordAsync(email, currentPass, newPass);
            return StatusCode(data.StatusCode, data);
        }
    }
}
