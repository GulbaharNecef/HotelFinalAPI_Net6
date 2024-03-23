using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GenericResponseModel<bool>> CreateRole(string name)
        {
            var data = await _roleManager.CreateAsync(new AppRole { Id = Guid.NewGuid().ToString(), Name = name });
            if (data.Succeeded)
                return new() { Data = data.Succeeded, Message = "Role created", StatusCode = 201 };
            else
                return new() { Data = data.Succeeded, Message = "Role creation failed!", StatusCode = 400 };
        }
        public async Task<GenericResponseModel<bool>> DeleteRoleById(string id)
        {
            GenericResponseModel<bool> response = new() { Data = false, StatusCode = 400, Message = "Deletion failed" };
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var data = await _roleManager.DeleteAsync(role);
                if (data.Succeeded)
                    return new() { Data = data.Succeeded, Message = "Role deleted", StatusCode = 200 };
                else
                    return new() { Data = data.Succeeded, StatusCode = 200 };
            }
            return response;
        }

        public async Task<GenericResponseModel<object>> GetAllRoles()
        {
            GenericResponseModel<object> response = new() { Data = null, Message = "Getting roles failed", StatusCode = 404 };
            var data = await _roleManager.Roles.ToListAsync();
            if (data != null)
                return new() { Data = data, StatusCode = 200, Message = "Getting roles successful" };
            else
                return response;
        }

        public async Task<GenericResponseModel<object>> GetRoleById(string id)
        {
            GenericResponseModel<object> response = new() { Data = null, Message = "Getting role failed", StatusCode = 400 };
            var data = await _roleManager.FindByIdAsync(id);
            if (data != null)
                return new() { Data = data, StatusCode = 200, Message = "Getting role successful" };
            return response;
        }

        public async Task<GenericResponseModel<bool>> UpdateRole(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                role.Name = name;
                var data = await _roleManager.UpdateAsync(role);
                if (data.Succeeded)
                    return new() { Data = data.Succeeded, Message = "Updating role successful", StatusCode = 200 };
                else
                    return new() { Data = data.Succeeded, Message = "Updating role failed", StatusCode = 400 };
            }
            else
                return new() { Data = false, Message = "Role doesn't exists", StatusCode = 200 };
        }
    }
}
