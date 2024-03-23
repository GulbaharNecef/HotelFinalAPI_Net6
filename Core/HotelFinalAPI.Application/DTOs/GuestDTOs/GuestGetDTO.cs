using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.GuestDTOs
{
    public class GuestGetDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? SpecialRequests { get; set; }
        public string? EmergencyContact { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        //public List<ReservationGetDTO> Reservations { get; set; } // Eager loaded reservations
        //public List<BillGetDTO> Bills { get; set; } // Eager loaded bills
    }
}
