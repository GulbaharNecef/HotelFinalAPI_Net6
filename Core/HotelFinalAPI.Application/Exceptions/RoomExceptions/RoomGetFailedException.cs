using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.RoomExceptions
{
    public class RoomGetFailedException:GetFailedException
    {
        public RoomGetFailedException() : base("Getting failed") { }
        public RoomGetFailedException(string message) : base(message) { }
        public RoomGetFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
