using HotelFinalAPI.Domain.Enums;
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
        public RoomTypes RoomType { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
    }
}
