using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class ReservationService : IReservationService
    {
        public Task<GenericResponseModel<ReservationCreateDTO>> CreateReservation(ReservationCreateDTO reservationCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> DeleteReservationById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<ReservationGetDTO>>> GetAllReservations()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<ReservationGetDTO>> GetReservationById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<ReservationUpdateDTO>> UpdateReservation(string id, ReservationUpdateDTO reservationUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
