using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.Enums;
using HotelFinalAPI.Persistance.Implementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllGuests()
        {
            var result = await _guestService.GetAllGuests();
            return StatusCode(result.StatusCode, result);
            //todo
            //return StatusCode((await _guestService.GetAllGuests()).StatusCode);

        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> GetGuestById(string id)
        {
            var result = await _guestService.GetGuestById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> CreateGuest(GuestCreateDTO guestCreateDTO)
        {
            var result = await _guestService.CreateGuest(guestCreateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> UpdateGuest(string id, GuestUpdateDTO guestUpdateDTO)
        {
            var result = await _guestService.UpdateGuest(id, guestUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> DeleteGuest(string id)
        {
            var result = await _guestService.DeleteGuestById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
