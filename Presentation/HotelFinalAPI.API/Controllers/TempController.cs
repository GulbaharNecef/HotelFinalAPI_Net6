using HotelFinalAPI.Application.IRepositories;
using HotelFinalAPI.Application.IRepositories.IEmployeeRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
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
        private readonly ILogger<TempController> _logger;

        public TempController(IUnitOfWork unitOfWork, ILogger<TempController> logger)
        {
            _unitOfWork = unitOfWork;
            _employeeReadRepository = _unitOfWork.GetReadRepository<Employee>();   
            _employeeWriteRepository = _unitOfWork.GetWriteRepository<Employee>();
            _logger = logger;
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
            //_logger.LogTrace("This Gulbahar trace");
            //_logger.LogDebug("Seri Log is Working -> Debug");
            //_logger.LogInformation("Seri Log is Working -> Information");
            //_logger.LogWarning("Seri Log is Working -> Warning");
            //_logger.LogError("Seri Log is Working -> Error");
            //_logger.LogCritical("Seri Log is Working -> Critical");

            Employee emp = await _employeeReadRepository.GetByIdAsync(id, false);
            emp.FirstName = "Eyyub";
            await _unitOfWork.SaveChangesAsync();
            return emp;
        }

        [HttpPost("[action]")]
        public async Task AddEmployee()
        {
            _logger.LogInformation("Check message");
            await _employeeWriteRepository.AddAsync(new() { FirstName = "Ilqar", LastName = "Agayev", Email = "iagayev@gmail.com", Role = "Night auditor"});
            await _unitOfWork.SaveChangesAsync();
        }


    }
}
