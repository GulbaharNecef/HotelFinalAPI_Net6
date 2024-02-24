using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IRoomService
    {
        Task<GenericResponseModel<List<RoomGetDTO>>> GetAllRooms();
        Task<GenericResponseModel<RoomGetDTO>> GetRoomById(string id);
        Task<GenericResponseModel<RoomCreateDTO>> CreateRoom(RoomCreateDTO roomCreateDTO);
        Task<GenericResponseModel<bool>> UpdateRoom(string id, RoomUpdateDTO roomUpdateDTO);
        Task<GenericResponseModel<bool>> DeleteRoom(string id);
    }
}
