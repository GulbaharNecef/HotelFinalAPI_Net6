using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IBillService
    {
        public Task<GenericResponseModel<List<BillGetDTO>>> GetAllBills();
        public Task<GenericResponseModel<BillGetDTO>> GetBillById(string id);
        public Task<GenericResponseModel<BillGetDTO>> GetBillsByGuestId(string guestId);
        public Task<GenericResponseModel<BillGetDTO>> GetBillsByPaidStatus(string status);//Repository deki GetWhere ile ola biler 
        public Task<GenericResponseModel<BillCreateDTO>> CreateBill(BillCreateDTO billCreateDTO);
        public Task<GenericResponseModel<BillUpdateDTO>> UpdateBill(BillUpdateDTO billUpdateDTO);
        public Task<GenericResponseModel<bool>> DeleteBillById(string id);
                        
    }
}
