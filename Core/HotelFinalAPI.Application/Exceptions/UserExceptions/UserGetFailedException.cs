using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.UserExceptions
{
    public class UserGetFailedException:GetFailedException
    {
        public UserGetFailedException() : base("Getting failed") { }
        public UserGetFailedException(string message) : base(message) { }
        public UserGetFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
