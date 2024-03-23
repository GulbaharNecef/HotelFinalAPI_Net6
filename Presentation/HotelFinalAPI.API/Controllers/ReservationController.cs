using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using HotelFinalAPI.Application.Enums;
using HotelFinalAPI.Persistance.Implementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservations();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> GetReservationById(string id)
        {
            var result = await _reservationService.GetReservationById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("by-guest-id/{guestId}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> GetReservationsByGuestId(string guestId)
        {
            var result = await _reservationService.GetReservationsByGuestId(guestId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> CreateReservation(ReservationCreateDTO reservationCreateDTO)
        {
            var result = await _reservationService.CreateReservation(reservationCreateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> UpdateReservation(string id, ReservationUpdateDTO reservationUpdateDTO)
        {
            var result = await _reservationService.UpdateReservation(id, reservationUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> DeleteReservationById(string id)
        {
            var result = await _reservationService.DeleteReservationById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("after-checkout")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> GetReservationAfterCheckOut()
        {
            var result = await _reservationService.GetReservationAfterCheckOut();
            return StatusCode(result.StatusCode, result);
        }
    }
}
