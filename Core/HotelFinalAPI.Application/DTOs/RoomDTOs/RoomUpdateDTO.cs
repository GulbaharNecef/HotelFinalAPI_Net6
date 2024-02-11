using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.DTOs.RoomDTOs
{
    public class RoomUpdateDTO
    {
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string Status { get; set; }
    }
}
