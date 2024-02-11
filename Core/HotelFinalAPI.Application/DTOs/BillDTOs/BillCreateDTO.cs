using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.BillDTOs
{
    public class BillCreateDTO
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool PaidStatus { get; set; }
        public Guid GuestId { get; set; }
    }
}
