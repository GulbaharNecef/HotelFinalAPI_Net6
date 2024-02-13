using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class GuestService : IGuestService
    {
        public Task<GenericResponseModel<GuestCreateDTO>> CreateGuest(GuestCreateDTO guestCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> DeleteGuestById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<GuestGetDTO>>> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<GuestGetDTO>> GetGuestById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<GuestUpdateDTO>> UpdateGuest(string id, GuestUpdateDTO guestUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
