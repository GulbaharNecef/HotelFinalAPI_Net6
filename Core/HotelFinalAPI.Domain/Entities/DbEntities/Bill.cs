using HotelFinalAPI.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Domain.Entities.DbEntities
{
    public class Bill:BaseEntity
    {
        //public Guid BillId { get; set; }
        public decimal Amount { get; set; }//decimal qalsinmi? bunu default olaraq db de decimal(18,2) yaradacaq
        public bool PaidStatus { get; set; }//bit de ola biler
        public Guid GuestId { get; set; }//foreign key
        //public Guid ReservationId { get; set; }//foreign key
        public Guest Guest { get; set; } //navigation property
       // public Reservation Reservation { get; set; }//navigation property

    }
}
