using HotelFinalAPI.Application.DTOs.UserDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IUserService
    {
        Task<GenericResponseModel<UserCreateResponseDTO>> Register(UserCreateDTO model);//Create user
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);//default olaraq interfacede methodlar public dir
        Task<GenericResponseModel<List<UserGetDTO>>> GetAllUsersAsync();
        Task<GenericResponseModel<bool>> AssignUserToRoleAsync(string userId, string[] roles);
        Task<GenericResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName);
        Task<GenericResponseModel<bool>> DeleteUserAsync(string userIdOrName);
        Task<GenericResponseModel<bool>> UpdateUserAsync(UserUpdateDTO model);
        //Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);//todo see again

        Task<string> GetCurrentSessionUserId(IdentityDbContext<AppUser, AppRole, string> dbContext);
    }
}
