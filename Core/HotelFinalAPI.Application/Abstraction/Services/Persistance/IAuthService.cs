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
        public Task<GenericResponseModel<TokenDTO>> Login(string username, string password/*, int accessTokenLifeTime*/);
        public Task<GenericResponseModel<TokenDTO>> RefreshTokenLoginAsync(string refreshToken);
    }
}
