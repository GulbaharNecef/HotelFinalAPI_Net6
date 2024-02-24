using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.BillDTOs
{
    public class BillUpdateDTO
    {
        Guid Id { get; set; }//todo bunu yoxlamaga yaziram silecem
        public decimal Amount { get; set; }
        public bool PaidStatus { get; set; }
    }
}
