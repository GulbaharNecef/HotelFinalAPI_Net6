using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.RoomExceptions
{
    public class RoomNotFoundException:NotFoundException
    {
        public RoomNotFoundException() : base("No rooms found!") { }
        public RoomNotFoundException(string roomId) : base($"The room with id : {roomId} doesn't exists.") { }
        public RoomNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
