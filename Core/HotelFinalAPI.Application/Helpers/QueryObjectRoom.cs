using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Helpers
{
    public class QueryObjectRoom
    {
        public decimal? MinPrice { get; set; } = 0;
        public decimal? MaxPrice { get; set; } = 10000; //Decimal.MaxValue;
        public string? Status { get; set; } = null;
        public string? RoomType { get; set; } = null;

    }
}
