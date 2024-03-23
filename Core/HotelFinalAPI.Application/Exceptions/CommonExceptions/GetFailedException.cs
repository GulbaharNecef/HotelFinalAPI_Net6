using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.CommonExceptions
{
    public class GetFailedException:Exception
    {
        public GetFailedException() { }

        public GetFailedException(string id) : base(id) { }

        public GetFailedException(string message, Exception inner) : base(message, inner) { }
    }
}
