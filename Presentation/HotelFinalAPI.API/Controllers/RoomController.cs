using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Application.Enums;
using HotelFinalAPI.Application.Helpers;
using HotelFinalAPI.Application.RequestParameters;
using HotelFinalAPI.Persistance.Implementation.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> GetAllRooms()
        {
            var result = await _roomService.GetAllRooms();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAllRooms([FromQuery] QueryObject query)
        {
            var result = await _roomService.GetRoomsFiltered(query);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("id")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> GetRoomById(string id)
        {
            var result = await _roomService.GetRoomById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> CreateRoom(RoomCreateDTO roomCreateDTO)
        {
            var result = await _roomService.CreateRoom(roomCreateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateRoom(string id, RoomUpdateDTO roomUpdateDTO)
        {
            var result = await _roomService.UpdateRoom(id, roomUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("after-checkout-{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateRoomAfterCheckOut(string id)
        {
            var result = await _roomService.UpdateRoomAfterCheckOut(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteRoomById(string id)
        {
            var result = await _roomService.DeleteRoomById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-range")]
        public async Task<IActionResult> GetRoomsPaged([FromQuery] Pagination pageDetails)
        {
            var result = await _roomService.GetRoomsRange(pageDetails);
            return StatusCode(result.StatusCode, result);
        }
    }
}
