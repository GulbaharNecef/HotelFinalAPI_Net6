using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.CommonExceptions
{
    public class InvalidIdFormatException : Exception
    {
        public InvalidIdFormatException() { }

        public InvalidIdFormatException(string id) : base($"Invalid id: {id}") { }

        public InvalidIdFormatException(string message, Exception inner) : base(message, inner) { }
    }
}
