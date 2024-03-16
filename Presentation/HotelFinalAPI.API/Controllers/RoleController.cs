using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.EmployeeDTOs;
using HotelFinalAPI.Persistance.Implementation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinalAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.GetAllRoles();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var result = await _roleService.GetRoleById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(string name)
        {
            var result = await _roleService.CreateRole(name);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateRole(string id, string name)
        {
            var result = await _roleService.UpdateRole(id, name);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteRoleById(string id)
        {
            var result = await _roleService.DeleteRoleById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
