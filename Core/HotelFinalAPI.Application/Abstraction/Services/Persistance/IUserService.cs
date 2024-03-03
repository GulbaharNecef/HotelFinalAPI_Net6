using HotelFinalAPI.Application.DTOs.UserDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Persistance
{
    public interface IUserService
    {
        public Task<GenericResponseModel<UserCreateResponseDTO>> CreateUser(UserCreateDTO model);
        public Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);//default olaraq interfacede methodlar public dir
    }
}
