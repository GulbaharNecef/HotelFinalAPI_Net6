using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Helpers
{
    public class QueryObject
    {
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public string? Status { get; set; } = null;
        public string? RoomType { get; set; } = null;

    }
}
