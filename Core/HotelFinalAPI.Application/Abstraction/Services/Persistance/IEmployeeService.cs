using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IEmployeeService
    {
        public Task<GenericResponseModel<List<EmployeeGetDTO>>> GetAllEmployees();
        public Task<GenericResponseModel<EmployeeGetDTO>> GetEmployeeById(string id);
        public Task<GenericResponseModel<EmployeeCreateDTO>> CreateEmployee(EmployeeCreateDTO employeeCreateDTO);
        public Task<GenericResponseModel<EmployeeUpdateDTO>> UpdateEmployee(string id, EmployeeUpdateDTO employeeCreateDTO);
        public Task<GenericResponseModel<bool>> DeleteEmployeeById(string id);

    }
}
