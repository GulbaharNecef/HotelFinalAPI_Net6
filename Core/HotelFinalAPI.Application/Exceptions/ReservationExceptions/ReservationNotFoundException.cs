using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.ReservationExceptions
{
    public class ReservationNotFoundException:NotFoundException
    {
        public ReservationNotFoundException() : base("No reservations found!") { }
        public ReservationNotFoundException(string reservationId) : base($"The reservation with id : {reservationId} doesn't exists.") { }
        public ReservationNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
