using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.IRepositories.IEmployeeRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeReadRepository _employeeReadRepository;
        private readonly IEmployeeWriteRepository _employeeWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public EmployeeService(IEmployeeReadRepository employeeReadRepository, IEmployeeWriteRepository employeeWriteRepository, IUnitOfWork unitOfWork)
        {
            _employeeReadRepository = employeeReadRepository;
            _employeeWriteRepository = employeeWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseModel<EmployeeCreateDTO>> CreateEmployee(EmployeeCreateDTO employeeCreateDTO)
        {
            GenericResponseModel<EmployeeCreateDTO> response = new()
            {
                Data = null,
                Message = "Unsuccessful operation",
                StatusCode = 400
            };
            //todo EmployeeCreateDTO null gele bilermi? if yes configure
            var employee = new Employee()
            {
                FirstName = employeeCreateDTO.FirstName,
                LastName = employeeCreateDTO.LastName,
                Email = employeeCreateDTO.Email,
                Role = employeeCreateDTO.Role,
            };
            await _employeeWriteRepository.AddAsync(employee);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = employeeCreateDTO;
                response.StatusCode = 200;
                response.Message = "Employee Created";
                return response;
            }

            return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteEmployeeById(string id)
        {
            //GenericResponseModel<bool> response = new();
            //var employee = await _employeeReadRepository.GetByIdAsync(id);
            //if(employee is not null)
            //{
            //    var deletedEmployee = await _employeeWriteRepository.RemoveByIdAsync(id);
            //    var affectedRows = await _unitOfWork.SaveChangesAsync();
            //    if (affectedRows > 0)
            //    {
            //        response.Data = deletedEmployee;
            //        response.StatusCode = 200;
            //        response.Message = "Emplo";
            //    }
            //}
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
