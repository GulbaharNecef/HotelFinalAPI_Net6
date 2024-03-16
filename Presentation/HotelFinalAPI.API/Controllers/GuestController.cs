using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Persistance.Implementation.Services;
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
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllGuests()
        {
            //var result = await _guestService.GetAllGuests();
            //return StatusCode(result.StatusCode, result);
            return StatusCode((await _guestService.GetAllGuests()).StatusCode);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGuestById(string id)
        {
            var result = await _guestService.GetGuestById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateGuest(GuestCreateDTO guestCreateDTO)
        {
            var result = await _guestService.CreateGuest(guestCreateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateGuest(string id, GuestUpdateDTO guestUpdateDTO)
        {
            var result = await _guestService.UpdateGuest(id, guestUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteGuest(string id)
        {
            var result = await _guestService.DeleteGuestById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
