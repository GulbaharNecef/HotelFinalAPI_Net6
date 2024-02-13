using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<GenericResponseModel<EmployeeCreateDTO>> CreateEmployee(EmployeeCreateDTO employeeCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> DeleteEmployeeById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<EmployeeGetDTO>>> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<EmployeeGetDTO>> GetEmployeeById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<EmployeeUpdateDTO>> UpdateEmployee(string id, EmployeeUpdateDTO employeeCreateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
