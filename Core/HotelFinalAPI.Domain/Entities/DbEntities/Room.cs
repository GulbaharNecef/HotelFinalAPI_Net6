﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Domain.Entities.DbEntities
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string Status { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}