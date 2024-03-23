using HotelFinalAPI.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Domain.Entities.DbEntities
{
    public class Reservation:BaseEntity
    {
        //public Guid ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid GuestId { get; set; }//foreign key
        public Guid RoomId { get; set; }//foreign key
        //public Guid BillId {get;set;}
        public Guest Guest { get; set; } //navigation prop
        public Room Room { get; set; }//navigation prop
    }
}
