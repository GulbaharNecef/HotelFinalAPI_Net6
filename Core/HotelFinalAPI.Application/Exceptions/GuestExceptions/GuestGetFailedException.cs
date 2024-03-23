using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.GuestExceptions
{
    public class GuestGetFailedException:GetFailedException
    {
        public GuestGetFailedException():base("Getting failed") { }
        public GuestGetFailedException(string message):base(message) { }
        public GuestGetFailedException(string message, Exception innerException) :base(message, innerException){ }
    }
}
