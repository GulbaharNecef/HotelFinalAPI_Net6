using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Persistance.Implementation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRooms()
        {
            var result = await _roomService.GetAllRooms();
            return StatusCode(result.StatusCode, result);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoomById(string id)
        {
            var result = await _roomService.GetRoomById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRoom(RoomCreateDTO roomCreateDTO)
        {
            var result = await _roomService.CreateRoom(roomCreateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRoom(string id, RoomUpdateDTO roomUpdateDTO)
        {
            var result = await _roomService.UpdateRoom(id, roomUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteRoomById(string id)
        {
            var result = await _roomService.DeleteRoomById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
