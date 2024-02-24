using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions
{
    public class CustomException<T>: Exception where T : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message,  Exception innerException) : base(message, innerException) { }
        public override string Message => base.Message;
         
    }
}
