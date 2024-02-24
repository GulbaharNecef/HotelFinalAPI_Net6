using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.GuestDTOs;
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

        public Task<GenericResponseModel<GuestCreateDTO>> CreateGuest(GuestCreateDTO guestCreateDTO)
        {
            throw new NotImplementedException();
            //GenericResponseModel<GuestCreateDTO> response = new()
            //{
            //    Data = null,
            //    Message = "Unsuccessful operation",
            //    StatusCode = 400
            //};
            ////if (guestCreateDTO == null) { throw new ArgumentNullException(); }
            //var bill = new Guest()
            //{
                
            //};
            //await _billWriteRepository.AddAsync(bill);
            //int affectedRows = await _unitOfWork.SaveChangesAsync();
            //if (affectedRows > 0)
            //{
            //    response.Data = billCreateDTO;
            //    response.StatusCode = 200;
            //    response.Message = "Bill Created";
            //    return response;
            //}
            //else
            //{
            //    return response;
            //}
        }

        public Task<GenericResponseModel<bool>> DeleteGuestById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<GuestGetDTO>>> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<GuestGetDTO>> GetGuestById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<GuestUpdateDTO>> UpdateGuest(string id, GuestUpdateDTO guestUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
