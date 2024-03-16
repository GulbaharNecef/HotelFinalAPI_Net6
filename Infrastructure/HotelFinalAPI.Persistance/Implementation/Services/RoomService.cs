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
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Domain.Enums;
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
            var room = new Room()
            {
                RoomNumber = roomCreateDTO.RoomNumber,
                RoomType = Enum.Parse<RoomTypes>(roomCreateDTO.RoomType),
                Price = roomCreateDTO.Price,
                Status = roomCreateDTO.Status
            };
            await _roomWriteRepository.AddAsync(room);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = roomCreateDTO;
                response.StatusCode = 200;
                response.Message = "Room Created";
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

            var rooms = _roomReadRepository.GetAll(false).ToList();
            if (rooms.Count() > 0)//Count()=>Linq; Count=>ICollection)
            {
                var roomGetDTO = _mapper.Map<List<RoomGetDTO>>(rooms);
                response.Data = roomGetDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            throw new RoomNotFoundException();
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
            updatedRoom.RoomType = roomUpdateDTO.RoomType;
            updatedRoom.Price = roomUpdateDTO.Price;
            updatedRoom.Status = roomUpdateDTO.Status;

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
    }
}
