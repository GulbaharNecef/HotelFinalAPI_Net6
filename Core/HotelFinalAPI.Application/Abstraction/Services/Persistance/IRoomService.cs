using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Application.Helpers;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Application.RequestParameters;
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
        Task<GenericResponseModel<List<RoomGetDTO>>> GetRoomsFiltered(QueryObject query);
        Task<GenericResponseModel<RoomCreateDTO>> CreateRoom(RoomCreateDTO roomCreateDTO);
        Task<GenericResponseModel<RoomUpdateDTO>> UpdateRoom(string id, RoomUpdateDTO roomUpdateDTO);
        Task<GenericResponseModel<bool>> UpdateRoomAfterCheckOut(string id);
        Task<GenericResponseModel<bool>> DeleteRoomById(string id);
        Task<GenericResponseModel<List<RoomGetDTO>>> GetRoomsRange(Pagination pageDetails);
    }
}
