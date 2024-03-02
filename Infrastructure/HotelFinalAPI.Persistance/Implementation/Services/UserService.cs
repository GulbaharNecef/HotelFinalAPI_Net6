using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.UserDTOs;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<GenericResponseModel<UserCreateResponseDTO>> CreateUser(UserCreateDTO model)
        {
            var user = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName

            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            GenericResponseModel<UserCreateResponseDTO> response = new() { Data = new UserCreateResponseDTO { Succeeded = result.Succeeded, Message = result.Succeeded ? "Successful" : "Unsuccessful" } };
            if (result.Succeeded)
            {
                response.StatusCode = 200;
                response.Message = "User created successfully";
                return response;
            }
            else
            {
                response.StatusCode = 400;
                response.Message = "Couldn't create user";
                return response;
            }

        }
    }
}
