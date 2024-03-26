using AutoMapper;
using FluentValidation;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.Exceptions.RoomExceptions;
using HotelFinalAPI.Application.IRepositories;
using HotelFinalAPI.Application.IRepositories.IEmployeeRepos;
using HotelFinalAPI.Application.IRepositories.IGuestRepos;
using HotelFinalAPI.Application.IRepositories.IReservationRepos;
using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Application.RequestParameters;
using HotelFinalAPI.Application.Validators.Bills;
using HotelFinalAPI.Application.Validators.Employees;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using HotelFinalAPI.Domain.Enums;
using HotelFinalAPI.Persistance.Repositories.GuestRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   //[Authorize(AuthenticationSchemes = "Admin")]
    public class TempController : ControllerBase
    {
        
        IReadRepository<Employee> _employeeReadRepository;
        IWriteRepository<Employee> _employeeWriteRepository;
        private readonly IGuestWriteRepository _guestWriteRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser > _userManager;
        private readonly IGuestService guestService;
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly IRoomReadRepository _roomReadRepository;
        private readonly IRoomWriteRepository _roomWriteRepository;   

        //IUnitOfWork<Employee> _unitOfWork;
        IUnitOfWork _unitOfWork;
        private readonly ILogger<TempController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public TempController(IUnitOfWork unitOfWork, ILogger<TempController> logger,IMapper mapper,IWebHostEnvironment webHostEnvironment, IGuestWriteRepository guestWriteRepository, UserManager<AppUser> userManager, IGuestService guestService, IReservationReadRepository reservationReadRepository, IRoomReadRepository roomReadRepository, IRoomWriteRepository roomWriteRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeReadRepository = _unitOfWork.GetReadRepository<Employee>();   
            _employeeWriteRepository = _unitOfWork.GetWriteRepository<Employee>();
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _guestWriteRepository = guestWriteRepository;
            _userManager = userManager;
            this.guestService = guestService;
            _reservationReadRepository = reservationReadRepository;
            _roomReadRepository = roomReadRepository;
            _roomWriteRepository = roomWriteRepository;
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
            _logger.LogTrace("This Gulbahar trace");
            _logger.LogDebug("Seri Log is Working -> Debug");
            _logger.LogInformation("Seri Log is Working -> Information");
            _logger.LogWarning("Seri Log is Working -> Warning");
            _logger.LogError("Seri Log is Working -> Error");
            _logger.LogCritical("Seri Log is Working -> Critical");

            Employee emp = await _employeeReadRepository.GetByIdAsync(id, false);
            emp.FirstName = "Eyyub";
            await _unitOfWork.SaveChangesAsync();
            return emp;
        }

        [HttpPost("[action]")]
        public async Task<bool> AddEmployee(EmployeeCreateDTO employee)
        {
            //var validator = new EmployeeCreateValidator();
            //var validationResult = validator.Validate(employee);
            if (ModelState.IsValid)
            {

                await _employeeWriteRepository.AddAsync(new Employee()
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email=employee.Email,
                    Role=employee.Role
                    
                });
                int result = await _unitOfWork.SaveChangesAsync();
                if(result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
          
           // _logger.LogInformation("Check message");
            //await _employeeWriteRepository.AddAsync(new() { FirstName = "samir", LastName = "Agayev", Email = "iagayev@gmail.com", Role = "Night auditor"});
            
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllEmployees()
        {
           
            return Ok(  _employeeReadRepository.GetAll(false).Select(x => new EmployeeGetDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Role=x.Role,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate
            } ));

            //return Ok(await _employeeReadRepository.GetAll().Select(x => new
            //{
            //    x.Id,
            //    x.FirstName,
            //    x.LastName,
            //}));
            
            //var employeesList = _employeeReadRepository.GetAll().ToListAsync();
            //List<EmployeeGetDTO> employees = _mapper.Map<List<EmployeeGetDTO>>(employeesList); //mapper kullanmak kod tekrarinin qarsisin aldi 😒🙄:|
            //return Ok(employees);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRangeEmployees([FromQuery]Pagination pagination)
        {
            _logger.LogInformation("kdjjadskshks");
            var employeesList = await _employeeReadRepository.GetAll(false).ToListAsync();
            List<EmployeeGetDTO> employees = _mapper.Map<List<EmployeeGetDTO>>(employeesList);
            employees = employees.Skip(pagination.Page * pagination.Total).Take(pagination.Total).ToList();//ToList
            return Ok(employees);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {//todo bunu copy eledim tam basa dusmek ucun again look through, bunu sonra istesen IFileService kimi Applicationa cixart,sonra Da Infratructure=>Infrastructure de implement ele [GY-27,28]
         //wwwroot a goturur bizi
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (IFormFile file in Request.Form.Files)
            {
                Random r = new();
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);//bufferSize 1024.
                //Ilgili file yi fileStreamdan hedef Path e basiyorum
                await file.CopyToAsync(fileStream);
                //filestreami temizleme islemi
                await fileStream.FlushAsync();
            }
            
            return Ok();

            /*
            try
            {
                var files = Request.Form.Files; // Form data-dan faylları alır

                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/images");
                if(!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
                foreach (var file in files)
                {
                    // Faylın adını təyin edirik
                    Random r = new();
                    string fileNamewithExtension = $"{r.Next()}{Path.GetExtension(file.FileName)}";

                    // Faylı wwwroot/resource/images qovluğuna yükləyirik
                    string fullPath = Path.Combine(uploadPath, fileNamewithExtension);
                    using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                }

                return Ok("Fayllar uğurla yükləndi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Xəta baş verdi: {ex.Message}");
            }*/
        }

        //reservasiya elemey isteyende checkkin ve check out date gondersin, mende available olan otaqlari return birde dolu olub amma check out date si gonderilen check in datesinden kicik olan otaqlari reurn edim 

        [HttpPost]
        public async Task<IActionResult> AddGuest(GuestCreateDTO guestCreateDTO)
        {
            GenericResponseModel<GuestCreateDTO> response =new();
            var guest = await  _guestWriteRepository.AddAsync(new Guest()
            {
                FirstName = guestCreateDTO.FirstName,
                LastName = guestCreateDTO.LastName,
                Email = "lkdlsjgdfl",
                Phone = guestCreateDTO.Phone,
                Country = guestCreateDTO.Country,
                DateOfBirth = DateTime.Parse("jdskfkjsks"),
                SpecialRequests = guestCreateDTO.SpecialRequests,
                EmergencyContact = guestCreateDTO.EmergencyContact
                
            });
            var affected = await _unitOfWork.SaveChangesAsync();
            if(affected> 0)
            {
                response.Data = guestCreateDTO;
            }

            return Ok(response);
        }
        [HttpGet]
        public async Task<string> MethodForResetPasswordTemp(string email)
        {//todo GeneratePasswordResetTokenAsync istifade etmek ucun AddIdentity de AddDefaultTokenProviders() yazilmalidir
            var user = await _userManager.FindByEmailAsync(email);
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            Console.WriteLine(resetToken);
            return resetToken;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGuest(string id, GuestUpdateDTO guestUpdateDTO)
        {
            var result = await guestService.UpdateGuest(id, guestUpdateDTO);
            return Ok(result);
        }

        [HttpGet("get-reservations")]
        public List<Reservation> getreserves()
        {
            var reservations = _reservationReadRepository.GetAll(false)
                           .Include(r => r.Guest)
                           .ThenInclude(r => r.Bills).ToList();
                           //.FirstOrDefaultAsync(r => r.RoomId == reservation.RoomId);
            return reservations;
        }

        
        [HttpGet("[action]")]
        public async Task<IActionResult> getAllRooms()
        {
            GenericResponseModel<List<RoomGet>> response = new();
            
            var rooms = await _roomReadRepository.GetAll(false).ToListAsync();
            if (rooms.Count() > 0)
            {
                //var roomGetDTO = _mapper.Map<List<RoomGet>>(rooms);
                List<RoomGet> list = new List<RoomGet>();
                foreach(var room in rooms)
                {
                    var roomGetDTO = new RoomGet()
                    {
                        Id = room.Id,
                        Price = room.Price,
                        CreatedDate = room.CreatedDate,
                        RoomNumber = room.RoomNumber,
                        RoomType = room.RoomType.ToString(),
                        Status = room.Status.ToString(),
                        UpdatedDate = room.UpdatedDate,

                    };
                    list.Add(roomGetDTO);
                }
               
                response.Data = list;
                response.StatusCode = 200;
                response.Message = "Successful";
                return Ok(list);
            }
            else
            {
                response.Data = null;
                response.StatusCode = 404;
                response.Message = "No rooms found.";
                
            }
            throw new RoomGetFailedException();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoomById(string id)
        {
            var room = await _roomReadRepository.GetByIdAsync(id);
            return Ok(room);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateRoom(string id, RoomGet model)
        {

            var updatedRoom = await _roomReadRepository.GetByIdAsync(id);
            if (updatedRoom is null)
                throw new RoomNotFoundException(id);

            updatedRoom.RoomNumber = model.RoomNumber;
            updatedRoom.RoomType = Enum.Parse<RoomTypes>(model.RoomType);
            updatedRoom.Price = model.Price;
            updatedRoom.Status = Enum.Parse<RoomStatus>(model.Status);

            _roomWriteRepository.Update(updatedRoom);
            return Ok(updatedRoom);
        }
    }

    

      /*  public async Task<GenericResponseModel<GuestUpdateDTO>> UpdateGuest(string id, GuestUpdateDTO guestUpdateDTO)
        {
            GenericResponseModel<GuestUpdateDTO> response = new();
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid result))
                throw new InvalidIdFormatException(id);

            var updatedGuest = await _guestReadRepository.GetByIdAsync(id);
            if (updatedGuest is null)
            {
                response.Data = null;
                response.StatusCode = 400;
                response.Message = "Istediyim kimi ol";
                return response;
            }
            //throw new GuestNotFoundException(id);

            updatedGuest.FirstName = guestUpdateDTO.FirstName;
            updatedGuest.LastName = guestUpdateDTO.LastName;
            updatedGuest.Email = guestUpdateDTO.Email;
            updatedGuest.Phone = guestUpdateDTO.Phone;
            updatedGuest.DateOfBirth = guestUpdateDTO.DateOfBirth;
            updatedGuest.EmergencyContact = guestUpdateDTO.EmergencyContact;
            updatedGuest.Country = guestUpdateDTO.Country;
            updatedGuest.SpecialRequests = guestUpdateDTO.SpecialRequests;

            _guestWriteRepository.Update(updatedGuest);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = guestUpdateDTO;
                response.StatusCode = 200;
                response.Message = "Guest Updated successfully";
                return response;
            }
            throw new Exception("Unexpected error occurred while updating the guest.");//todo bu bele best practicedirmi acaba🤔
        }*/
    }


public class RoomGet
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; }
    public string RoomType { get; set; }
    public string Status { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; } 
}
