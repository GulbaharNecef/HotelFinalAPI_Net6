using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.ReservationExceptions
{
    public class ReservationGetFailedException:GetFailedException
    {
        public ReservationGetFailedException() : base("Getting failed") { }
        public ReservationGetFailedException(string message) : base(message) { }
        public ReservationGetFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
