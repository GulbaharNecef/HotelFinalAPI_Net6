using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.UserDTOs
{
    public class UserCreateResponseDTO
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
