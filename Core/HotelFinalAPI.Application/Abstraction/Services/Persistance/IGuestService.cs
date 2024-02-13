using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IGuestService
    {
        public Task<GenericResponseModel<List<GuestGetDTO>>> GetAllGuests();
        public Task<GenericResponseModel<GuestGetDTO>> GetGuestById(string id);
        public Task<GenericResponseModel<GuestCreateDTO>> CreateGuest(GuestCreateDTO guestCreateDTO);
        public Task<GenericResponseModel<GuestUpdateDTO>> UpdateGuest(string id, GuestUpdateDTO guestUpdateDTO);
        public Task<GenericResponseModel<bool>> DeleteGuestById(string id);


    }
}
