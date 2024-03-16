using HotelFinalAPI.Domain.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Exceptions.EmployeeExceptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException() : base("No employees found!") { }
        public EmployeeNotFoundException(string employeeId) : base($"The bill with id : {employeeId} doesn't exists.") { }
        public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
