using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using HotelFinalAPI.Application.Exceptions.BillExceptions;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.Exceptions.ReservationExceptions;
using HotelFinalAPI.Application.IRepositories.IGuestRepos;
using HotelFinalAPI.Application.IRepositories.IReservationRepos;
using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly IGuestReadRepository _guestReadRepository;
        private readonly IRoomReadRepository _roomReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IReservationReadRepository reservationReadRepository, IReservationWriteRepository reservationWriteRepository, IGuestReadRepository guestReadRepository,IRoomReadRepository roomReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _reservationReadRepository = reservationReadRepository;
            _reservationWriteRepository = reservationWriteRepository;
            _guestReadRepository = guestReadRepository;
            _roomReadRepository = roomReadRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenericResponseModel<ReservationCreateDTO>> CreateReservation(ReservationCreateDTO reservationCreateDTO)
        {//todo do i need to check that if reservationCreateDTO is null???🤔
            GenericResponseModel<ReservationCreateDTO> response = new()
            {
                Data = null,
                Message = "Unsuccessful operation while creating Bill",
                StatusCode = 400
            };
            var validGuest = await _guestReadRepository.GetByIdAsync(reservationCreateDTO.GuestId);
            if(validGuest != null)
            {
                var validRoom = await _roomReadRepository.GetByIdAsync(reservationCreateDTO.RoomId);
                if(validRoom != null)
                {
                    var reservation = new Reservation()
                    {
                        CheckInDate = reservationCreateDTO.CheckInDate,
                        CheckOutDate = reservationCreateDTO.CheckOutDate,
                        GuestId = Guid.Parse(reservationCreateDTO.GuestId),
                        RoomId = Guid.Parse(reservationCreateDTO.RoomId)
                    };
                    await _reservationWriteRepository.AddAsync(reservation);
                    int affectedRows = await _unitOfWork.SaveChangesAsync();
                    if (affectedRows > 0)
                    {
                        response.Data = reservationCreateDTO;
                        response.StatusCode = 200;
                        response.Message = "Reservation Created";
                        return response;
                    }
                }
            }
            return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteReservationById(string id)
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

            var deletedReservation = await _reservationReadRepository.GetByIdAsync(id);
            if (deletedReservation is null)
                throw new ReservationNotFoundException(id);

            _reservationWriteRepository.Remove(deletedReservation);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = true;
                response.StatusCode = 200;
                response.Message = "Reservation deleted successfully";
                return response;
            }
            return response;
        }

        public async Task<GenericResponseModel<List<ReservationGetDTO>>> GetAllReservations()
        {
            GenericResponseModel<List<ReservationGetDTO>> response = new();

            var reservations = _reservationReadRepository.GetAll(false).ToList();
            if (reservations.Count() > 0)//Count()=>Linq; Count=>ICollection)
            {
                var reservationGetDTO = _mapper.Map<List<ReservationGetDTO>>(reservations);
                response.Data = reservationGetDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            throw new ReservationNotFoundException();
        }

        public async Task<GenericResponseModel<ReservationGetDTO>> GetReservationById(string id)
        {
            GenericResponseModel<ReservationGetDTO> response = new();

            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid validId))
                throw new InvalidIdFormatException(id);

            var reservation = await _reservationReadRepository.GetByIdAsync(id);
            if (reservation != null)
            {
                var reservationDTO = _mapper.Map<ReservationGetDTO>(reservation);
                response.Data = reservationDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
                throw new ReservationNotFoundException(id);
        }

        public async Task<GenericResponseModel<ReservationUpdateDTO>> UpdateReservation(string id, ReservationUpdateDTO reservationUpdateDTO)
        {
            GenericResponseModel<ReservationUpdateDTO> response = new();
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid result))
                throw new InvalidIdFormatException(id);

            var updatedReservation = await _reservationReadRepository.GetByIdAsync(id);
            if (updatedReservation is null)
                throw new ReservationNotFoundException(id);

            updatedReservation.CheckInDate = reservationUpdateDTO.CheckInDate;
            updatedReservation.CheckOutDate = reservationUpdateDTO.CheckOutDate;
            updatedReservation.GuestId = Guid.Parse(reservationUpdateDTO.GuestId);
            updatedReservation.RoomId = Guid.Parse(reservationUpdateDTO.RoomId);

            _reservationWriteRepository.Update(updatedReservation);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = reservationUpdateDTO;
                response.StatusCode = 200;
                response.Message = "Reservation Updated successfully";
                return response;
            }
            throw new Exception("Unexpected error occurred while updating the Reservation.");//todo bu bele best practicedirmi acaba🤔
        }
    }
}
