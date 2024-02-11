using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Models.ResponseModels
{
    public class GenericResponseModel<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        //public GenericResponseModel()
        //{
        //    StatusCode = 200; // Default success status code
        //    Message = null;
        //    Data = default(T);
        //}

    }
}
