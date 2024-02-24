using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.BillExceptions
{
    public class BillNotFoundException:NotFoundException 
    {
        //public string? ErrorCode { get; set; }
        public BillNotFoundException() { }
        public BillNotFoundException(string billId): base ($"The bill with id : {billId} doesn't exists.") { }
        public BillNotFoundException(string message, Exception innerException): base (message, innerException) { }

        
        //public override string Message
        //    => base.Message;


    }
}
