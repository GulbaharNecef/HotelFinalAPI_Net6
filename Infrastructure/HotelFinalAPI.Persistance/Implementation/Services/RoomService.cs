using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Application.Exceptions.BillExceptions;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.Exceptions.RoomExceptions;
using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Application.RequestParameters;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{

    public class RoomService : IRoomService
    {
        private readonly IRoomReadRepository _roomReadRepository;
        private readonly IRoomWriteRepository _roomWriteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<Room> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IRoomReadRepository roomReadRepository, IRoomWriteRepository roomWriteRepository, IMapper mapper, ILogger<Room> logger, IUnitOfWork unitOfWork)
        {
            _roomReadRepository = roomReadRepository;
            _roomWriteRepository = roomWriteRepository;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseModel<RoomCreateDTO>> CreateRoom(RoomCreateDTO roomCreateDTO)
        {
            GenericResponseModel<RoomCreateDTO> response = new()
            {
                Data = null,
                Message = "Unsuccessful operation while creating Room",
                StatusCode = 400
            };
            if (Enum.TryParse<RoomTypes>(roomCreateDTO.RoomType, out RoomTypes roomType) && Enum.TryParse<RoomStatus>(roomCreateDTO.Status, out RoomStatus roomStatus))
            {
                var room = new Room()
                {
                    RoomNumber = roomCreateDTO.RoomNumber,
                    RoomType = roomType,
                    Price = roomCreateDTO.Price,
                    Status = roomStatus
                };
                await _roomWriteRepository.AddAsync(room);
                int affectedRows = await _unitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    response.Data = roomCreateDTO;
                    response.StatusCode = 200;
                    response.Message = "Room Created";
                }
            }
            else
            {
                response.Message = "Enter valid RoomType or RoomStatus";
                return response;
            }
            return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteRoomById(string id)
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

            var deletedRoom = await _roomReadRepository.GetByIdAsync(id);
            if (deletedRoom is null)
                throw new RoomNotFoundException(id);

            _roomWriteRepository.Remove(deletedRoom);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = true;
                response.StatusCode = 200;
                response.Message = "Room deleted successfully";
                return response;
            }
            return response;
        }

        public async Task<GenericResponseModel<List<RoomGetDTO>>> GetAllRooms()
        {
            GenericResponseModel<List<RoomGetDTO>> response = new();

            var rooms = await _roomReadRepository.GetAll(false).ToListAsync();
            if (rooms.Count() > 0)//Count()=>Linq; Count=>ICollection)
            {
                var roomGetDTO = _mapper.Map<List<RoomGetDTO>>(rooms);
                response.Data = roomGetDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
            {
                response.Data = null;
                response.StatusCode = 404;
                response.Message = "No rooms found.";
                return response;
            }
            throw new RoomGetFailedException();
        }

        public async Task<GenericResponseModel<RoomGetDTO>> GetRoomById(string id)
        {
            GenericResponseModel<RoomGetDTO> response = new();

            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid validId))
                throw new InvalidIdFormatException(id);

            var room = await _roomReadRepository.GetByIdAsync(id);
            if (room != null)
            {
                var roomDTO = _mapper.Map<RoomGetDTO>(room);
                response.Data = roomDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
                throw new RoomNotFoundException(id);
        }

        public async Task<GenericResponseModel<RoomUpdateDTO>> UpdateRoom(string id, RoomUpdateDTO roomUpdateDTO)
        {
            GenericResponseModel<RoomUpdateDTO> response = new();
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid result))
                throw new InvalidIdFormatException(id);

            var updatedRoom = await _roomReadRepository.GetByIdAsync(id);
            if (updatedRoom is null)
                throw new RoomNotFoundException(id);

            updatedRoom.RoomNumber = roomUpdateDTO.RoomNumber;
            updatedRoom.RoomType = Enum.Parse<RoomTypes>(roomUpdateDTO.RoomType);
            updatedRoom.Price = roomUpdateDTO.Price;
            updatedRoom.Status = Enum.Parse<RoomStatus>(roomUpdateDTO.Status);

            _roomWriteRepository.Update(updatedRoom);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = roomUpdateDTO;
                response.StatusCode = 200;
                response.Message = "Room Updated successfully";
                return response;
            }
            throw new Exception("Unexpected error occurred while updating the room.");//todo bu bele best practicedirmi acaba🤔
        }

        public async Task<GenericResponseModel<List<RoomGetDTO>>> GetRoomsRange(Pagination pageDetails)
        {
            GenericResponseModel<List<RoomGetDTO>> response = new();
            var rooms = await _roomReadRepository.GetAll().ToListAsync();
            var roomGetDTO = _mapper.Map<List<RoomGetDTO>>(rooms);
            var pagedRooms = roomGetDTO.Skip(pageDetails.Page * pageDetails.Total).Take(pageDetails.Total).ToList();
            response.Data = pagedRooms;
            response.StatusCode = 200;
            response.Message = "Getting paged rooms successful";
            return response;
        }
    }
}
