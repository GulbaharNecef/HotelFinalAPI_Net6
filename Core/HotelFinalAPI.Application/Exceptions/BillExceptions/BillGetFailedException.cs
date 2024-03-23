using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.BillExceptions
{
    public class BillGetFailedException:GetFailedException
    {
        public BillGetFailedException() : base("Getting failed") { }
        public BillGetFailedException(string message) : base(message) { }
        public BillGetFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
