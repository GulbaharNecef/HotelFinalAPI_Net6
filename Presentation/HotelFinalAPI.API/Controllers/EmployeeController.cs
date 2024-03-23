using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin", Roles = $"{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var result = await _employeeService.GetAllEmployees();
            return StatusCode(result.StatusCode, result);

        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDTO employeeCreateDTO)
        {
            var result = await _employeeService.CreateEmployee(employeeCreateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateEmployee(string id, EmployeeUpdateDTO employeeUpdateDTO)
        {
            var result = await _employeeService.UpdateEmployee(id, employeeUpdateDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteEmployeeById(string id)
        {
            var result = await _employeeService.DeleteEmployeeById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
