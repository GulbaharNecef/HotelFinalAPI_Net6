using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class RoomService : IRoomService
    {
        public Task<GenericResponseModel<RoomGetDTO>> CreateRoom(RoomCreateDTO roomCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> DeleteRoom(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<RoomGetDTO>>> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<RoomGetDTO>> GetRoomById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> UpdateRoom(string id, RoomUpdateDTO roomUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
