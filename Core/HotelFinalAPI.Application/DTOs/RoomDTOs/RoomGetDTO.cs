using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.RoomDTOs
{
    public class RoomGetDTO
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string Status { get; set; }
        public ICollection<ReservationGetDTO> Reservations { get; set; }// RoomGetDTO includes a nested DTO for Reservation
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; } //=DateTime.Now
    }
}
