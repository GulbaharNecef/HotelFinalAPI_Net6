using HotelFinalAPI.Application.IRepositories.IEmployeeRepos;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Implementation.Services.Repositories.EmployeeRepos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        IEmployeeReadRepository _employeeReadRepository;
        IEmployeeWriteRepository _employeeWriteRepository;

        public TempController(IEmployeeReadRepository employeeReadRepository, IEmployeeWriteRepository employeeWriteRepository)
        {
            _employeeReadRepository = employeeReadRepository;
            _employeeWriteRepository = employeeWriteRepository;
        }
        [HttpGet]
        public async Task Get()
        {
           await _employeeWriteRepository.AddRangeAsync(new()
            {
                new() { FirstName = "Ruqeyya", LastName = "Zeynalova", Email="rzeynalova@gmail.com", Role="Maintenance"},
                //new() {Id = Guid.NewGuid(), FirstName = "Tunar", LastName = "Elizade", Email="elizade@gmail.com", Role="Hotel Manager"}
            });
            var count = await _employeeWriteRepository.SaveAsync();
        }
        [HttpGet("GetById")]
        public async Task<Employee> GetById(string id = "7DD9B0A7-0549-4BBB-C8CE-08DC2971FC08")
        {
            Employee emp = await _employeeReadRepository.GetByIdAsync(id, false);
            emp.FirstName = "Nazile";
            await _employeeWriteRepository.SaveAsync();
            return emp;
        }

        [HttpPost]
        public async Task AddEmployee()
        {
            await _employeeWriteRepository.AddAsync(new() { FirstName = "Sevda", LastName = "Meherremli", Email = "sevdameherremli@gmail.com", Role = "Host or hostess" });
            await _employeeWriteRepository.SaveAsync();
        }

    }
}
