using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using HotelFinalAPI.Persistance.Implementation.Services;
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllReservations()
        {
            var result = await _reservationService.GetAllReservations();
            return StatusCode(result.StatusCode, result);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetReservationById(string id)
        {
            var result = await _reservationService.GetReservationById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateReservation(ReservationCreateDTO reservationCreateDTO)
        {
            var result = await _reservationService.CreateReservation(reservationCreateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateReservation(string id, ReservationUpdateDTO reservationUpdateDTO)
        {
            var result = await _reservationService.UpdateReservation(id, reservationUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteReservationById(string id)
        {
            var result = await _reservationService.DeleteReservationById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
