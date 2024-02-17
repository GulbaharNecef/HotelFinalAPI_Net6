using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.RequestParameters
{
    public record Pagination
    {
        public int Page { get; set; } = 0;
        public int Total { get; set; } = 5;//Size
    }
}
