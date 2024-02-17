using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.BillDTOs
{
    public class BillGetDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public bool PaidStatus { get; set; }
        public Guid GuestId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
