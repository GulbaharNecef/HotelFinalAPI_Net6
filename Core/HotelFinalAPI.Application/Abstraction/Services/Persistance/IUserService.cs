using HotelFinalAPI.Application.DTOs.UserDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
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
    }
}
