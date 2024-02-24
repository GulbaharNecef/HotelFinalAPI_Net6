using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.Exceptions.BillExceptions;
using HotelFinalAPI.Application.Exceptions.CommonExceptions;
using HotelFinalAPI.Application.IRepositories.IBillRepos;
using HotelFinalAPI.Application.IUnitOfWorks;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.DbEntities;
using HotelFinalAPI.Persistance.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class BillService : IBillService
    {
        private readonly IBillWriteRepository _billWriteRepository;
        private readonly IBillReadRepository _billReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILogger<Bill> _logger;


        public BillService(IBillWriteRepository billWriteRepository, IBillReadRepository billReadRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<Bill> logger)
        {
            _billWriteRepository = billWriteRepository;
            _billReadRepository = billReadRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericResponseModel<BillCreateDTO>> CreateBill(BillCreateDTO billCreateDTO)
        {
            GenericResponseModel<BillCreateDTO> response = new()
            {
                Data = null,
                Message = "Unsuccessful operation",
                StatusCode = 400
            };
            //if (billCreateDTO == null) { throw new ArgumentNullException(); }
            var bill = new Bill()
            {
                Amount = billCreateDTO.Amount,
                GuestId = Guid.Parse(billCreateDTO.GuestId),
                PaidStatus = billCreateDTO.PaidStatus
            };
            await _billWriteRepository.AddAsync(bill);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = billCreateDTO;
                response.StatusCode = 200;
                response.Message = "Bill Created";
                return response;
            }

                return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteBillById(string id)
        {
            GenericResponseModel<bool> response = new()
            {
                Data = false,
                StatusCode = 400,
                Message = "Unsuccessful"
            };
            if (id is null)
            {
                return response;
            }
            var deletedBill = await _billReadRepository.GetByIdAsync(id);
            if (deletedBill == null)
            {
                return response;
            }

            _billWriteRepository.Remove(deletedBill);
            int affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = true;
                response.StatusCode = 200;
                response.Message = "Bill deleted successfully";
                return response;
            }
            return response;
        }

        public async Task<GenericResponseModel<List<BillGetDTO>>> GetAllBills()
        {
            GenericResponseModel<List<BillGetDTO>> response = new()
            {
                Data = null,
                Message = "Unsuccessful operation",
                StatusCode = 400
            };
            var bills = _billReadRepository.GetAll(false).ToList();
            if (bills == null)
            {
                throw new BillNotFoundException();
            }
            var billGetDTO = _mapper.Map<List<BillGetDTO>>(bills);

            response.Data = billGetDTO;
            response.StatusCode = 200;
            response.Message = "Successful";
            return response;
        }

        public async Task<GenericResponseModel<BillGetDTO>> GetBillById(string id)
        {
            try
            {
                GenericResponseModel<BillGetDTO> response = new()
                {
                    Data = null,
                    StatusCode = 400,
                    Message = "Unsuccessfull"
                };
                //if (string.IsNullOrEmpty(id))
                //{
                //    throw new BillNotFoundException(response.Message); // null gele bilmir validasiyaya gore ona gore heleki bunu ignore edirem
                //}

                //throw new BillNotFoundException(Guid.Parse(id));
                var isValidId = Guid.TryParse(id, out Guid validId);
                if (isValidId)
                {
                    var bill = await _billReadRepository.GetByIdAsync(id);
                    var billDTO = _mapper.Map<BillGetDTO>(bill);
                    if (bill != null)
                    {
                        response.Data = billDTO;
                        response.StatusCode = 200;
                        response.Message = "Successful";
                        return response;
                    }
                    else
                        throw new BillNotFoundException(id);
                }
                else
                    throw new InvalidIdFormatException(id);

            }
            catch (BillNotFoundException e)
            {
                return new GenericResponseModel<BillGetDTO>
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500
                };
            }
            catch (InvalidIdFormatException e)
            {
                Console.WriteLine(e.Message);
                //_logger.LogInformation(e.Message);

                return new GenericResponseModel<BillGetDTO>
                {
                    Data = null,
                    Message = e.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<GenericResponseModel<List<BillGetDTO>>> GetBillsByGuestId(string guestId)
        {
            GenericResponseModel<List<BillGetDTO>> response = new()
            {
                Data = null,
                StatusCode = 400,
                Message = "Unsuccessful"
            };
            //if (guestId == null)
            //{
            //    throw new BillNotFoundException($"with guestId{guestId}");
            //}
            if (Guid.TryParse(guestId, out Guid result))
            {
                var billsByGuest = _billReadRepository.Table.Where(b => b.GuestId == result).ToList();
                if (billsByGuest.Count > 0)
                {
                    var billByGuestDTOList = _mapper.Map<List<BillGetDTO>>(billsByGuest);
                    response.Data = billByGuestDTOList;
                    response.StatusCode = 200;
                    response.Message = "Successful to get Bills by GuestId";
                    return response;
                }
                else
                {//burda exception atacam, heleki bunu yazdim
                    response.Data = null;
                    response.StatusCode = 404;
                    response.Message = $"There is no bill related to id: {guestId}";
                }
            }
            throw new BillNotFoundException();
        }

        public async Task<GenericResponseModel<List<BillGetDTO>>> GetBillsByPaidStatus(string status)
        {
            GenericResponseModel<List<BillGetDTO>> response = new()
            {
                Data = null,
                StatusCode = 400,
                Message = "Unsuccessful"
            };
            //if (status == null)
            //    return response;
            if (bool.TryParse(status, out bool result))
            {
                var billsByPaidStatus = await _billReadRepository.Table.Where(b => b.PaidStatus == result).ToListAsync();//ToList mi? yes axi IQueryabledir mi? ne elaqeee:)
                var billsGetDTO = _mapper.Map<List<BillGetDTO>>(billsByPaidStatus);
                if(billsByPaidStatus.Any()) //checks if there are any bills found
                {
                    response.Data = billsGetDTO;
                    response.StatusCode = 200;
                    response.Message = "Successful";
                    return response;
                }
                else 
                    throw new BillNotFoundException();//todo bu exceptionu burda coxluq ucun atmaq duzgundurmu? yes id qebul etmeyen constructor ise dusecek
            }
            else
            {
                return new GenericResponseModel<List<BillGetDTO>>
                {
                    Data = null,
                    StatusCode = 400,
                    Message = "Enter true or false as status of Bill"
                };
            }
        }

        public async Task<GenericResponseModel<BillUpdateDTO>> UpdateBill(string id, BillUpdateDTO billUpdateDTO)
        {
            GenericResponseModel<BillUpdateDTO> response = new()
            {
                Data = null,
                StatusCode = 400,
                Message = "Unsuccesful"
            };
            if (id == null || billUpdateDTO == null)
                return response;

            var updatedBill = await _billReadRepository.GetByIdAsync(id);
            if (updatedBill == null)
                return response;

            updatedBill.Amount = billUpdateDTO.Amount;
            updatedBill.PaidStatus = billUpdateDTO.PaidStatus;

            _billWriteRepository.Update(updatedBill);
            var affectedRows = await _unitOfWork.SaveChangesAsync();
            if (affectedRows > 0)
            {
                response.Data = billUpdateDTO;
                response.StatusCode = 200;
                response.Message = "Bill Updated successfully";
                return response;
            }
            return response;

        }
    }
}
