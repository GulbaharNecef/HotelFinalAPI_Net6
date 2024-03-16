using HotelFinalAPI.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Domain.Entities.DbEntities
{
    public class Guest:BaseEntity
    {
       // public Guid GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? SpecialRequests { get; set; }
        public string? EmergencyContact { get; set; }
        //todo PaymentInformation 
        public ICollection<Reservation> Reservations { get; set; } //navigation property meselen guesti onun Reservation lari ile birge getirmek istesem eager loading
        public ICollection<Bill> Bills { get; set; }
    }
}
