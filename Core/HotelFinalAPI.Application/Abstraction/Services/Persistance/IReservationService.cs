using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IReservationService
    {
        public Task<GenericResponseModel<List<ReservationGetDTO>>> GetAllReservations();
        public Task<GenericResponseModel<ReservationGetDTO>> GetReservationById(string id);
        public Task<GenericResponseModel<List<ReservationGetDTO>>> GetReservationsByGuestId(string guestId);
        public Task<GenericResponseModel<List<ReservationGetDTO>>> GetReservationAfterCheckOut();
        public Task<GenericResponseModel<ReservationCreateDTO>> CreateReservation(ReservationCreateDTO reservationCreateDTO);
        public Task<GenericResponseModel<ReservationUpdateDTO>> UpdateReservation(string id, ReservationUpdateDTO reservationUpdateDTO);
        public Task<GenericResponseModel<bool>> DeleteReservationById(string id);


    }
}
