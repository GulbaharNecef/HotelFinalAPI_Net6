using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.RoomDTOs;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.Exceptions.RoomExceptions;
using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.DbEntities;
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
                StatusCode = 400,
                Message = "Unsuccessful"
            };
            var room = new Room()
            {
                Price = roomCreateDTO.Price,
                RoomNumber = roomCreateDTO.RoomNumber,
                RoomType = roomCreateDTO.RoomType,
                Status = roomCreateDTO.Status
            };
            await _roomWriteRepository.AddAsync(room);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = roomCreateDTO;
                response.StatusCode = 200;
                response.Message = "Room created successfully";
                return response;
            }
            return response;//bunun yerine bidene exception at , cunki microsofda gordum uazmisdi ki null return etmekdense exception throw ele
        }

        public async Task<GenericResponseModel<bool>> DeleteRoom(string id)
        {
            GenericResponseModel<bool> response = new();
            if (Guid.TryParse(id, out var _))
            {
                var deletedRoom = await _roomReadRepository.GetByIdAsync(id);
                if (deletedRoom is null)
                    throw new RoomNotFoundException($"Room with id: {id} not found");

                _roomWriteRepository.Remove(deletedRoom);
                //await _roomWriteRepository.RemoveByIdAsync(id);
                var affectedRows = await _unitOfWork.SaveChangesAsync();
                if (affectedRows > 0)
                {
                    response.Data = true;
                    response.StatusCode = 200;
                    response.Message = $"The room with ID: {id} deleted";
                }
                else
                {
                    response.Data = false;
                    response.StatusCode = 400;
                    response.Message = $"Deletion failed with ID: {id}";
                }
                return response;
            }
            else
                throw new InvalidIdFormatException(id);
        }

        public async Task<GenericResponseModel<List<RoomGetDTO>>> GetAllRooms()
        {
            GenericResponseModel<List<RoomGetDTO>> response = new();
            var rooms = _roomReadRepository.GetAll(false);
            if (rooms.Count() > 0)
            {
                var roomGetDTO = _mapper.Map<List<RoomGetDTO>>(rooms);
                response.Data = roomGetDTO;
                response.StatusCode = 200;
                response.Message = "Success";
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "No rooms found";
            }
            return response;
        }

        public async Task<GenericResponseModel<RoomGetDTO>> GetRoomById(string id)
        {
            GenericResponseModel<RoomGetDTO> response = new();
            var room = await _roomReadRepository.GetByIdAsync(id);
            if (room is not null)
            {
                var roomGetDTO = _mapper.Map<RoomGetDTO>(room);
                response.Data = roomGetDTO;
                response.StatusCode = 200;
                response.Message = "Success";
            }
            else
            {
                response.StatusCode = 404;
                response.Message = "No room found";
            }
            return response;
        }

        public async Task<GenericResponseModel<bool>> UpdateRoom(string id, RoomUpdateDTO roomUpdateDTO)
        {
            GenericResponseModel<bool> response = new()
            {
                Data = false,
                StatusCode = 400,
                Message = "Something went wrong"
            };
            var updatedRoom = await _roomReadRepository.GetByIdAsync(id, false);//false mi?
            if (updatedRoom is null) { throw new RoomNotFoundException($"Room with id: {id} didn't found"); }

            var room = new Room()
            {
                RoomNumber = updatedRoom.RoomNumber,
                RoomType = updatedRoom.RoomType,
                Price = updatedRoom.Price,
                Status = updatedRoom.Status
            };
            _roomWriteRepository.Update(room);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = true;
                response.StatusCode = 200;
                response.Message = $"The Room with ID: {id} updated successfully";
            }
            return response;
        }
    }
}
