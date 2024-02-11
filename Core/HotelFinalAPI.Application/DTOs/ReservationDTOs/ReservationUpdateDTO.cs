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
        public Guid GuestId { get; set; }
        public Guid RoomId { get; set; }
    }
}
