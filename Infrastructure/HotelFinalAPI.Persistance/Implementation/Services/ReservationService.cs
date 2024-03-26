using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.DTOs.ReservationDTOs;
using HotelFinalAPI.Application.Exceptions.BillExceptions;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.Exceptions.ReservationExceptions;
using HotelFinalAPI.Application.IRepositories.IBillRepos;
using HotelFinalAPI.Application.IRepositories.IGuestRepos;
using HotelFinalAPI.Application.IRepositories.IReservationRepos;
using HotelFinalAPI.Application.IRepositories.IRoomRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRoomWriteRepository _roomWriteRepository;
        private readonly IBillWriteRepository _billWriteRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationService(IReservationReadRepository reservationReadRepository, IReservationWriteRepository reservationWriteRepository, IGuestReadRepository guestReadRepository, IRoomReadRepository roomReadRepository, IUnitOfWork unitOfWork, IMapper mapper, IBillWriteRepository billWriteRepository, IRoomWriteRepository roomWriteRepository, IUserService userService)
        {
            _reservationReadRepository = reservationReadRepository;
            _reservationWriteRepository = reservationWriteRepository;
            _guestReadRepository = guestReadRepository;
            _roomReadRepository = roomReadRepository;
            _roomWriteRepository = roomWriteRepository;
            _billWriteRepository = billWriteRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<GenericResponseModel<ReservationCreateDTO>> CreateReservation(ReservationCreateDTO reservationCreateDTO)
        {
            GenericResponseModel<ReservationCreateDTO> response = new()
            {
                Data = null,
                Message = "Please, be sure that you enter valid data.",
                StatusCode = 400
            };
            if (await ValidateReservation(reservationCreateDTO))
            {
                using (var transaction = _unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        var reservation = new Reservation()
                        {
                            CheckInDate = reservationCreateDTO.CheckInDate,
                            CheckOutDate = reservationCreateDTO.CheckOutDate,
                            GuestId = Guid.Parse(reservationCreateDTO.GuestId),
                            RoomId = Guid.Parse(reservationCreateDTO.RoomId)
                        };
                        var createdReserv = await _reservationWriteRepository.AddAsync(reservation);
                        if (createdReserv)
                        {
                            var room = await _roomReadRepository.GetByIdAsync(reservationCreateDTO.RoomId);
                            room.Status = Domain.Enums.RoomStatus.Reserved;
                            _roomWriteRepository.Update(room);
                            //await _unitOfWork.SaveChangesAsync();

                            //Add bill
                            await _billWriteRepository.AddAsync(new()
                            {
                                Amount = await CalculateBill(reservationCreateDTO),
                                GuestId = Guid.Parse(reservationCreateDTO.GuestId),
                                PaidStatus = false
                            });

                            await _unitOfWork.SaveChangesAsync();
                            await _unitOfWork.CommitAsync();

                            response.Message = "Reservation created";
                            response.StatusCode = 201;
                            response.Data = reservationCreateDTO;
                            return response;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction if any operation fails
                        await _unitOfWork.RollbackAsync();
                        //todo loga yaz  
                        response.Message = "Error occurred while processing the reservation.";
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
            //var reservations = _reservationReadRepository.GetAll(false).ToList();
            var reservations = _reservationReadRepository.GetAll()
            .Include(r => r.Guest).Include(r => r.Room).ToList();

            /*Project the desired properties
            var reservationDTOs = reservations.Select(r => new ReservationGetDTO
            {
                GuestName = r.Guest.FirstName,
                RoomNumber = r.Room.RoomNumber,
                // Include other properties as needed
            }).ToList();*/

            if (reservations.Any())
            {
                var reservationGetDTO = _mapper.Map<List<ReservationGetDTO>>(reservations);
                //reservationGetDTO.GuestName = reservations.Select(Guest.)
                response.Data = reservationGetDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
            {
                response.Data = null;
                response.StatusCode = 200;
                response.Message = "No reservations found.";
                return response;
            }
            throw new ReservationGetFailedException();
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

        private async Task<bool> ValidateReservation(ReservationCreateDTO reservationCreateDTO)
        {

            if (!Guid.TryParse(reservationCreateDTO.GuestId, out _))
                throw new InvalidIdFormatException();
            if (!Guid.TryParse(reservationCreateDTO.RoomId, out _))
                throw new InvalidIdFormatException();
            var room = await _roomReadRepository.GetByIdAsync(reservationCreateDTO.RoomId);
            if (room != null && room.Status == Domain.Enums.RoomStatus.Available)
            {//todo otaq indi available olmaya biler amma belke check in check out geleceydedirse onda available olacaq
                var guest = await _guestReadRepository.GetByIdAsync(reservationCreateDTO.GuestId);
                if (guest != null)
                {
                    return true;
                }
            }
            else
                return false;
            return false;
        }

        private async Task<decimal> CalculateBill(ReservationCreateDTO reservationCreateDTO)
        {
            var room = await _roomReadRepository.GetByIdAsync(reservationCreateDTO.RoomId);
            var roomPrice = room.Price;
            var numberofNights = (int)(reservationCreateDTO.CheckOutDate - reservationCreateDTO.CheckInDate).TotalDays;
            decimal totalPrice = roomPrice * numberofNights;
            return totalPrice;
        }

        public async Task<GenericResponseModel<List<ReservationGetDTO>>> GetReservationAfterCheckOut()
        {
            GenericResponseModel<List<ReservationGetDTO>> response = new();
            //var reservations = _reservationReadRepository.GetAll(false).ToList();
            var reservations = _reservationReadRepository.GetWhere(r => r.CheckOutDate < DateTime.Now);

            if (reservations.Any())
            {
                var reservationGetDTO = _mapper.Map<List<ReservationGetDTO>>(reservations);
                response.Data = reservationGetDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
            {
                response.Data = null;
                response.StatusCode = 200;
                response.Message = "No reservations found.";
                return response;
            }
            throw new ReservationGetFailedException();
        }

        public async Task<GenericResponseModel<List<ReservationGetDTO>>> GetReservationsByGuestId(string guestId)
        {
            GenericResponseModel<List<ReservationGetDTO>> response = new();

            if (string.IsNullOrEmpty(guestId))
                throw new CustomArgumentNullException(guestId);

            if (!Guid.TryParse(guestId, out Guid validId))
                throw new InvalidIdFormatException(guestId);
            var reservation = await _reservationReadRepository.GetAll(false).Include(r => r.Guest).Include(r => r.Room).Where(r => r.GuestId == validId).ToListAsync();
            if (reservation != null)
            {
                var reservationDTO = _mapper.Map<List<ReservationGetDTO>>(reservation);
                response.Data = reservationDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
                throw new ReservationNotFoundException(guestId);
        }

        public async Task<GenericResponseModel<ReservationCancellationDTO>> CancelReservation(string reservationId)
        {
            GenericResponseModel<ReservationCancellationDTO> response = new()
            {
                Data = null,
                Message = "Unable to cancel the reservation.",
                StatusCode = 400
            };

            var reservation = await _reservationReadRepository.GetByIdAsync(reservationId);

            if (reservation == null)
            {
                response.Message = "Reservation not found.";
                response.StatusCode = 404;
                return response;
            }

            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    // Update room status
                    var room = await _roomReadRepository.GetByIdAsync(reservation.RoomId.ToString());
                    room.Status = RoomStatus.Available;
                    _roomWriteRepository.Update(room);

                    // Remove the reservation
                    var deleted = await _reservationWriteRepository.RemoveByIdAsync(reservationId);
                    if (!deleted)
                    {
                        response.Message = "Failed to cancel the reservation.";
                        return response;
                    }

                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();

                    response.Message = "Reservation cancelled successfully.You don't have to do anything.";
                    response.StatusCode = 200;
                    return response;
                }
                catch (Exception ex)
                {
                    // Rollback transaction if any operation fails
                    await _unitOfWork.RollbackAsync();
                    // Todo: Log error
                    response.Message = "An error occurred while processing the cancellation request.";
                    return response;
                }
            }
        }

        public async Task<GenericResponseModel<List<ReservationGetDTO>>> GetReservationByUserId(string userId)
        {
            GenericResponseModel<List<ReservationGetDTO>> response = new()
            {
                Data = null,
                Message = "Unable to get the reservation.",
                StatusCode = 400
            };

            if (string.IsNullOrEmpty(userId))
                throw new CustomArgumentNullException(userId);

            if (!Guid.TryParse(userId, out Guid validId))
                throw new InvalidIdFormatException(userId);

            var user = await _userService.GetUserById(userId);
            var reservation = await _reservationReadRepository.GetWhere(r => r.CreatedBy == user.Data.UserName).Include(r=>r.Room).Include(r=>r.Guest).ToListAsync();
            if (reservation == null)
            {
                response.Message = "Reservation didn't found.";
                return response;
            }

            response.Message = "Getting reservation successful.";
            response.Data = _mapper.Map<List<ReservationGetDTO>>(reservation);
            response.StatusCode = 200;
            return response;
        }
    }
}
