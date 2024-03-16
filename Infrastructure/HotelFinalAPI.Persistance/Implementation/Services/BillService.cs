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
                Message = "Unsuccessful operation while creating Bill",
                StatusCode = 400
            };
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
                Message = "Unsuccessful operation!"
            };
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);//nameof() parametrin adini goturur deyerini yox

            if (!Guid.TryParse(id, out Guid validId))
                throw new InvalidIdFormatException(id);

            var deletedBill = await _billReadRepository.GetByIdAsync(id);
            if (deletedBill is null)
                throw new BillNotFoundException(id);

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
            GenericResponseModel<List<BillGetDTO>> response = new();

            var bills = _billReadRepository.GetAll(false).ToList();
            if (bills.Count() > 0)//Count()=>Linq; Count=>ICollection)
            {
                var billGetDTO = _mapper.Map<List<BillGetDTO>>(bills);
                response.Data = billGetDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            throw new BillNotFoundException();
        }

        public async Task<GenericResponseModel<BillGetDTO>> GetBillById(string id)
        {//todo id default filter sayesinde null gele bilmir deye heleki bunu ignore edirem? am i wrong? ya da her ehtimala IsNullOrEmpty yoxlayiram :| 
            GenericResponseModel<BillGetDTO> response = new();

            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid validId))
                throw new InvalidIdFormatException(id);

            var bill = await _billReadRepository.GetByIdAsync(id);
            if (bill != null)
            {
                var billDTO = _mapper.Map<BillGetDTO>(bill);
                response.Data = billDTO;
                response.StatusCode = 200;
                response.Message = "Successful";
                return response;
            }
            else
                throw new BillNotFoundException(id);
        }

        public async Task<GenericResponseModel<List<BillGetDTO>>> GetBillsByGuestId(string guestId)
        {
            GenericResponseModel<List<BillGetDTO>> response = new();

            if (string.IsNullOrEmpty(guestId))
                throw new CustomArgumentNullException(guestId);

            if (!Guid.TryParse(guestId, out Guid validId))
                throw new InvalidIdFormatException(guestId);

            var billsByGuest = _billReadRepository.Table.Where(b => b.GuestId == validId).ToList();
            if (billsByGuest.Count > 0)//Count()=>Linq; Count=>ICollection)
            {
                var billByGuestDTOList = _mapper.Map<List<BillGetDTO>>(billsByGuest);
                response.Data = billByGuestDTOList;
                response.StatusCode = 200;
                response.Message = "Successful to get Bills by GuestId";
                return response;
            }
            else
                throw new BillNotFoundException();
        }

        public async Task<GenericResponseModel<List<BillGetDTO>>> GetBillsByPaidStatus(string status)
        {
            GenericResponseModel<List<BillGetDTO>> response = new();

            if (string.IsNullOrEmpty(status))
                throw new CustomArgumentNullException(status);

            if (bool.TryParse(status, out bool result))
            {
                var billsByPaidStatus = await _billReadRepository.Table.Where(b => b.PaidStatus == result).ToListAsync();//ToList mi? yes axi IQueryabledir mi? ne elaqeee:)
                if (billsByPaidStatus.Any()) //checks if there are any bills found
                {
                    var billsGetDTO = _mapper.Map<List<BillGetDTO>>(billsByPaidStatus);
                    response.Data = billsGetDTO;
                    response.StatusCode = 200;
                    response.Message = "Successful";
                    return response;
                }
                else
                    throw new BillNotFoundException();// bu exceptionu burda coxluq ucun atmaq duzgundurmu? yes id qebul etmeyen constructor ise dusecek
            }
            else
            {
                response.Data = null;
                response.StatusCode = 400;
                response.Message = "Status value Bill must be True or False!";
                return response;
            }
        }

        public async Task<GenericResponseModel<BillUpdateDTO>> UpdateBill(string id, BillUpdateDTO billUpdateDTO)
        {
            GenericResponseModel<BillUpdateDTO> response = new();
            if (string.IsNullOrEmpty(id))
                throw new CustomArgumentNullException(id);

            if (!Guid.TryParse(id, out Guid result))
                throw new InvalidIdFormatException(id);

            var updatedBill = await _billReadRepository.GetByIdAsync(id);
            if (updatedBill is null)
                throw new BillNotFoundException(id);

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
            throw new Exception("Unexpected error occurred while updating the bill.");//todo bu bele best practicedirmi acaba🤔
        }
    }
}
