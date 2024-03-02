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
        [HttpPost]
        public async Task<IActionResult> Login(string usernameOrEmail, string password,int accessTokenLifeTime)
        {
            //var result = await _authService.Login(usernameOrEmail, password);
           var data =  await _authService.Login(usernameOrEmail, password, accessTokenLifeTime);
            return StatusCode(data.StatusCode, data);
        }
    }
}
