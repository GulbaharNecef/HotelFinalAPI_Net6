using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.CommonExceptions
{
    public class CustomArgumentNullException : ArgumentNullException
    {
        public CustomArgumentNullException() : base() { }
        public CustomArgumentNullException(string paramName) : base($"The {paramName} parameter cannot be null or empty.", nameof(paramName)) { }//nameof() parametrin adini goturur deyerini yox
    }
}
