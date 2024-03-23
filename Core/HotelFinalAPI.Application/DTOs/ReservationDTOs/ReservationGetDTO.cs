using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.ReservationDTOs
{
    public class ReservationGetDTO
    {
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid GuestId { get; set; }
        public Guid RoomId { get; set; }
        //public GuestGetDTO Guest { get; set; }
        public string RoomNumber {  get; set; }
        public string GuestName { get; set; }
        //public RoomGetDTO Room { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
