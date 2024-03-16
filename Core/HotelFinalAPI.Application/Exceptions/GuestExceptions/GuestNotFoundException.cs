using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.GuestExceptions
{
    public class GuestNotFoundException:NotFoundException
    {
        public GuestNotFoundException() : base("No guests found!") { }
        public GuestNotFoundException(string guestId) : base($"The guest with id : {guestId} doesn't exists.") { }
        public GuestNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
