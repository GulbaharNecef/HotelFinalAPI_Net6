using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class BillService : IBillService
    {
        public Task<GenericResponseModel<BillCreateDTO>> CreateBill(BillCreateDTO billCreateDTO)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> DeleteBillById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<BillGetDTO>>> GetAllBills()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<BillGetDTO>> GetBillById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<BillGetDTO>> GetBillsByGuestId(string guestId)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<BillGetDTO>> GetBillsByPaidStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<BillUpdateDTO>> UpdateBill(string id, BillUpdateDTO billUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
