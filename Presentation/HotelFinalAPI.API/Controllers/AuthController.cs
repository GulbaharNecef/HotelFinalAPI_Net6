using HotelFinalAPI.API.RequestModels;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("login")]
        public async Task<IActionResult> Login( LoginModel model /*string usernameOrEmail,  string password*/ )
        {
            var data = await _authService.Login(model.usernameOrEmail, model.password);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> LoginWithRefreshToken([FromBody] string refreshToken)
        {
            var data = await _authService.RefreshTokenLoginAsync(refreshToken);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> Logout(string usernameOrEmail)
        {
            var data = await _authService.Logout(usernameOrEmail);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(string email, string currentPass, string newPass)
        {
            var data = await _authService.ResetPasswordAsync(email, currentPass, newPass);
            return StatusCode(data.StatusCode, data);
        }
    }
}
