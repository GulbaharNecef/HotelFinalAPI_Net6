using HotelFinalAPI.Application.DTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IAuthService
    {
        Task<GenericResponseModel<TokenDTO>> Login(string username, string password/*, int accessTokenLifeTime*/);
        Task<GenericResponseModel<TokenDTO>> RefreshTokenLoginAsync(string refreshToken);
        Task<GenericResponseModel<bool>> Logout(string usernameOrEmail);
        Task<GenericResponseModel<bool>> ResetPasswordAsync(string email, string currentPass, string newPass);
    }
}
