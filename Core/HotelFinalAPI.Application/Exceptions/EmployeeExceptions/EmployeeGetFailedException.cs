using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.EmployeeExceptions
{
    public class EmployeeGetFailedException:GetFailedException
    {
        public EmployeeGetFailedException() : base("Getting failed") { }
        public EmployeeGetFailedException(string message) : base(message) { }
        public EmployeeGetFailedException(string message, Exception innerException) : base(message, innerException) { }

    }
}
