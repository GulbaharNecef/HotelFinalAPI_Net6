using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
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

        [HttpPost("[action]")]
        public async Task<IActionResult> AddRoom(RoomCreateDTO roomCreateDTO)
        {
            var data = await _roomService.CreateRoom(roomCreateDTO);
            return StatusCode(data.StatusCode, data);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteRoom(string roomId)
        {
            var data = await _roomService.DeleteRoom(roomId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRoom(string roomId, RoomUpdateDTO roomUpdateDTO)
        {
            var data = await _roomService.UpdateRoom(roomId, roomUpdateDTO);
            return StatusCode(data.StatusCode, data);
        }
    }
}
