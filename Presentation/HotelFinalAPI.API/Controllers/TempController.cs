using HotelFinalAPI.Application.IRepositories;
using HotelFinalAPI.Application.IRepositories.IEmployeeRepos;
using HotelFinalAPI.Application.IUnitOfWork;
using HotelFinalAPI.Domain.Entities.DbEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        
        IReadRepository<Employee> _employeeReadRepository;
        IWriteRepository<Employee> _employeeWriteRepository;

        //IUnitOfWork<Employee> _unitOfWork;
        IUnitOfWork _unitOfWork;

        public TempController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
            _employeeReadRepository = _unitOfWork.GetReadRepository<Employee>();   
            _employeeWriteRepository = _unitOfWork.GetWriteRepository<Employee>();
        }
        [HttpPost("[action]")]
        public async Task AddEmployeeRange()
        {
           await _employeeWriteRepository.AddRangeAsync(new()
            {
                new() { FirstName = "Elmar", LastName = "Babayev", Email="ebabayev@gmail.com", Role="General Manager"},
                //new() {Id = Guid.NewGuid(), FirstName = "Tunar", LastName = "Elizade", Email="elizade@gmail.com", Role="Hotel Manager"}
            });
            var count = await _unitOfWork.SaveChangesAsync();
        }
        [HttpGet("[action]")]
        public async Task<Employee> GetById(string id = "30F5D13B-4C16-4D11-8B37-08DC2A51128A")
        {
            Employee emp = await _employeeReadRepository.GetByIdAsync(id, false);
            emp.FirstName = "Eyyub";
            await _unitOfWork.SaveChangesAsync();
            return emp;
        }

        [HttpPost("[action]")]
        public async Task AddEmployee()
        {
            await _employeeWriteRepository.AddAsync(new() { FirstName = "Sevda", LastName = "Meherremli", Email = "sevdameherremli@gmail.com", Role = "Host or hostess" });
            await _unitOfWork.SaveChangesAsync();
        }


    }
}
