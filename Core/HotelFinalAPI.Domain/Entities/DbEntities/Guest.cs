using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Domain.Entities.DbEntities
{
    public class Guest
    {
        public Guid GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<Reservation> Reservations { get; set; } //navigation property meselen guesti onun
                                                                   //Reservation lari ile birge getirmek istesem 
                                                                   //eager loading
        public ICollection<Bill> Bills { get; set; }
    }
}
