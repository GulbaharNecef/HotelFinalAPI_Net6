using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs
{
    public class UserSessionDTO
    {
        public string? LoginName { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
