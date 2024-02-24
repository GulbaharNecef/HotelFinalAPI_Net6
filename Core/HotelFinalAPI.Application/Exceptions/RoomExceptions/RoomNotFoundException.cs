using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.RoomExceptions
{
    public class RoomNotFoundException:NotFoundException
    {
        public RoomNotFoundException() { }
        public RoomNotFoundException(string message) : base(message) { }
        public RoomNotFoundException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
