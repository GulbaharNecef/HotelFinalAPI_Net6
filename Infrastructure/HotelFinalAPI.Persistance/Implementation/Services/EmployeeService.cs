using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.Exceptions.BillExceptions;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.Exceptions.EmployeeExceptions;
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
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository, IEmployeeWriteRepository employeeWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _employeeReadRepository = employeeReadRepository;
            _employeeWriteRepository = employeeWriteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            GenericResponseModel<bool> response = new()
            {
                Data = false,
                StatusCode = 400,
                Message = "Unsuccessful operation!"
            };
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid validId))
                throw new InvalidIdFormatException(id);

            var deletedEmployee = await _employeeReadRepository.GetByIdAsync(id);
            if (deletedEmployee is null)
                throw new EmployeeNotFoundException(id);

            _employeeWriteRepository.Remove(deletedEmployee);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = true;
                response.StatusCode = 200;
                response.Message = "Employee deleted successfully";
                return response;
            }
            return response;
        }

        public async Task<GenericResponseModel<List<EmployeeGetDTO>>> GetAllEmployees()
        {
            GenericResponseModel<List<EmployeeGetDTO>> response = new();

            var bills = _employeeReadRepository.GetAll(false).ToList();
            if (bills.Count() > 0)//Count()=>Linq; Count=>ICollection)
            {
                var employeeGetDTO = _mapper.Map<List<EmployeeGetDTO>>(bills);
                response.Data = employeeGetDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
            {
                response.Data = null;
                response.StatusCode = 404;
                response.Message = "No employees found.";
                return response;
            }
            throw new EmployeeGetFailedException();
        }

        public async Task<GenericResponseModel<EmployeeGetDTO>> GetEmployeeById(string id)
        {
            GenericResponseModel<EmployeeGetDTO> response = new();

            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid validId))
                throw new InvalidIdFormatException(id);

            var employee = await _employeeReadRepository.GetByIdAsync(id);
            if (employee != null)
            {
                var employeeDTO = _mapper.Map<EmployeeGetDTO>(employee);
                response.Data = employeeDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
                throw new EmployeeNotFoundException(id);
        }

        public async Task<GenericResponseModel<EmployeeUpdateDTO>> UpdateEmployee(string id, EmployeeUpdateDTO employeeCreateDTO)
        {
            GenericResponseModel<EmployeeUpdateDTO> response = new();
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid result))
                throw new InvalidIdFormatException(id);

            var updatedEmployee = await _employeeReadRepository.GetByIdAsync(id);
            if (updatedEmployee is null)
                throw new EmployeeNotFoundException(id);

            updatedEmployee.FirstName = employeeCreateDTO.FirstName;
            updatedEmployee.LastName = employeeCreateDTO.LastName;
            updatedEmployee.Email = employeeCreateDTO.Email;
            updatedEmployee.Role = employeeCreateDTO.Role;

            _employeeWriteRepository.Update(updatedEmployee);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = employeeCreateDTO;
                response.StatusCode = 200;
                response.Message = "Employee Updated successfully";
                return response;
            }
            throw new Exception("Unexpected error occurred while updating the employee.");//todo bu bele best practicedirmi acaba🤔
        }
    }
}
