using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml;
using HotelFinalAPI.Application.Models.ResponseModels;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IRoleService
    {
        Task<GenericResponseModel<object>> GetAllRoles();
        Task<GenericResponseModel<object>> GetRoleById(string id);
        Task<GenericResponseModel<bool>> CreateRole(string name);
        Task<GenericResponseModel<bool>> UpdateRole(string id, string name);
        Task<GenericResponseModel<bool>> DeleteRoleById(string id);
    }
}
