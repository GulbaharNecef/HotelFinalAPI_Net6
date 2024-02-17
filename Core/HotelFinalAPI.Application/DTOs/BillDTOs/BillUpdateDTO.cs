using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.BillDTOs
{
    public class BillUpdateDTO
    {
        public decimal Amount { get; set; }
        public bool PaidStatus { get; set; }
    }
}
