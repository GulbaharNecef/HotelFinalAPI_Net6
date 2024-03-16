using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
using HotelFinalAPI.Application.Exceptions.BillExceptions;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.Exceptions.GuestExceptions;
using HotelFinalAPI.Application.IRepositories.IBillRepos;
using HotelFinalAPI.Application.IRepositories.IGuestRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.Repositories.BillRepos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestReadRepository _guestReadRepository;
        private readonly IGuestWriteRepository _guestWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILogger<Guest> _logger;

        public GuestService(IGuestReadRepository guestReadRepository, IGuestWriteRepository guestWriteRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<Guest> logger)
        {
            _guestReadRepository = guestReadRepository;
            _guestWriteRepository = guestWriteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResponseModel<GuestCreateDTO>> CreateGuest(GuestCreateDTO guestCreateDTO)
        {
            GenericResponseModel<GuestCreateDTO> response = new()
            {
                Data = null,
                Message = "Unsuccessful operation while creating Guest",
                StatusCode = 400
            };
            var guest = new Guest()
            {
                FirstName = guestCreateDTO.FirstName,
                LastName = guestCreateDTO.LastName,
                Email = guestCreateDTO.Email,
                Phone = guestCreateDTO.Phone,
                Country = guestCreateDTO.Country,
                EmergencyContact = guestCreateDTO.EmergencyContact,
                DateOfBirth = guestCreateDTO.DateOfBirth,
                SpecialRequests = guestCreateDTO.SpecialRequests,

            };
            await _guestWriteRepository.AddAsync(guest);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = guestCreateDTO;
                response.StatusCode = 200;
                response.Message = "Guest Created";
                return response;
            }

            return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteGuestById(string id)
        {
            GenericResponseModel<bool> response = new()
            {
                Data = false,
                StatusCode = 400,
                Message = "Unsuccessful operation!"
            };
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);//nameof() parametrin adini goturur deyerini yox

            if (!Guid.TryParse(id, out Guid validId))
                throw new InvalidIdFormatException(id);

            var deletedGuest = await _guestReadRepository.GetByIdAsync(id);
            if (deletedGuest is null)
                throw new GuestNotFoundException(id);

            _guestWriteRepository.Remove(deletedGuest);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = true;
                response.StatusCode = 200;
                response.Message = "Guest deleted successfully";
                return response;
            }
            return response;
        }

        public async Task<GenericResponseModel<List<GuestGetDTO>>> GetAllGuests()
        {
            GenericResponseModel<List<GuestGetDTO>> response = new();

            var guests = _guestReadRepository.GetAll(false).ToList();
            if (guests.Count() > 0)
            {
                var guestGetDTO = _mapper.Map<List<GuestGetDTO>>(guests);
                response.Data = guestGetDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            throw new GuestNotFoundException();
        }

        public async Task<GenericResponseModel<GuestGetDTO>> GetGuestById(string id)
        {
            GenericResponseModel<GuestGetDTO> response = new();

            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid validId))
                throw new InvalidIdFormatException(id);

            var guest = await _guestReadRepository.GetByIdAsync(id);
            if (guest != null)
            {
                var guestDTO = _mapper.Map<GuestGetDTO>(guest);
                response.Data = guestDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
                throw new GuestNotFoundException(id);
        }

        public async Task<GenericResponseModel<GuestUpdateDTO>> UpdateGuest(string id, GuestUpdateDTO guestUpdateDTO)
        {
            GenericResponseModel<GuestUpdateDTO> response = new();
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid result))
                throw new InvalidIdFormatException(id);

            var updatedGuest = await _guestReadRepository.GetByIdAsync(id);
            if (updatedGuest is null)
                throw new GuestNotFoundException(id);

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
        }
    }
}
