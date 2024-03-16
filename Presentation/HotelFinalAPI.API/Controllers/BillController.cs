using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.BillDTOs;
using HotelFinalAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        public readonly IBillService _billService;
        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBill([FromBody] BillCreateDTO billCreateDTO)
        {
            var result = await _billService.CreateBill(billCreateDTO);
            return StatusCode(result.StatusCode, result);
        }
        //todo do these on all endpoints at the end, https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio
        ///<summary>
        ///Gets all Bills
        ///</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBills()//todo ask GetAll repository da sinxron dur , burda asynchron yazmaq dogrumu?
        {
            var data = await _billService.GetAllBills();
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        [AuthorizeAttribute(Roles =$"{Roles.User},{Roles.Admin}")]// i donno way
        public async Task<IActionResult> GetBillsByGuestId(string guestId)
        {
            var data = await _billService.GetBillsByGuestId(guestId);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBillById(string id)
        {
            var data = await _billService.GetBillById(id);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBillsByPaidStatus(string status)
        {
            var data = await _billService.GetBillsByPaidStatus(status);
            return StatusCode(data.StatusCode, data);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateBill(string billId, BillUpdateDTO billUpdateDTO)
        {
            var data = await _billService.UpdateBill(billId, billUpdateDTO);
            return StatusCode(data.StatusCode, data);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteBillById(string billId)
        {
            var data = await _billService.DeleteBillById(billId);
            return StatusCode(data.StatusCode, data);
        }
    }
}
