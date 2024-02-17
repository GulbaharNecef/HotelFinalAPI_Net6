using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.ReservationDTOs
{
    public class ReservationUpdateDTO
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string GuestId { get; set; }
        public string RoomId { get; set; }
    }
}
