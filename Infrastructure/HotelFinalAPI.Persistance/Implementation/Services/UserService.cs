using AutoMapper;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs.UserDTOs;
using HotelFinalAPI.Application.Enums;
using HotelFinalAPI.Application.Exceptions;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<GenericResponseModel<bool>> AssignUserToRoleAsync(string userId, string[] roles)
        {
            GenericResponseModel<bool> response = new() { Data = false, StatusCode = 400, Message = "Assign user to roles failed" };
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                var result = await _userManager.AddToRolesAsync(user, roles);
                response.Data = result.Succeeded;
                response.StatusCode = 200;
                response.Message = "Assign user to roles successfull";
            }
            else
                throw new UserNotFoundException();

            return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteUserAsync(string userIdOrName)
        {
            GenericResponseModel<bool> response = new();
            var user = await _userManager.FindByEmailAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);
            if (user == null)
                throw new UserNotFoundException();
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                response.Data = result.Succeeded;
                response.StatusCode = 200;
                response.Message = "User deleted";
                return response;
            }
            else
            {
                response.Data = result.Succeeded;
                response.StatusCode = 400;
                response.Message = "User deletion failed";
                return response;
            }
            throw new Exception("Error happened while deleting User");//todo change to custom exception
        }

        public async Task<GenericResponseModel<List<UserGetDTO>>> GetAllUsersAsync()
        {
            GenericResponseModel<List<UserGetDTO>> response = new();
            var users = await _userManager.Users.ToListAsync();
            if (users is null)
                throw new UserNotFoundException("Users not found!");
            if (users.Count > 0)
            {
                var userGetDTO = _mapper.Map<List<UserGetDTO>>(users);
                response.Data = userGetDTO;
                response.StatusCode = 200;
                response.Message = "Getting users successful";
                return response;
            }
            throw new UserNotFoundException("Users not found!");
        }

        public async Task<GenericResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            GenericResponseModel<string[]> response = new() { Data = null, StatusCode = 400, Message = "Getting roles failed" };
            var user = await _userManager.FindByIdAsync(userIdOrName);
            if (user is null)
                user = await _userManager.FindByNameAsync(userIdOrName);
            if (user is not null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                response.Data = userRoles.ToArray();
                response.StatusCode = 200;
                response.Message = "Getting roles successful";
                return response;
            }
            else { throw new UserNotFoundException(); }
            //return response;
        }

        public async Task<GenericResponseModel<UserCreateResponseDTO>> Register(UserCreateDTO model)
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
            GenericResponseModel<UserCreateResponseDTO> response = new() { /*Data = new UserCreateResponseDTO { Succeeded = result.Succeeded, Message = result.Succeeded ? "Successful" : "Unsuccessful" } */};

            response.StatusCode = result.Succeeded ? 200 : 400;
            response.Data = new UserCreateResponseDTO { Succeeded = true, Message = "Successful" };

            if (!result.Succeeded)
            {
                response.Message = string.Join("/n", result.Errors.Select(e => $"{e.Code} - {e.Description}"));
                return response;
            }

            var newUser = await _userManager.FindByNameAsync(model.Username);
            if(newUser == null)
                newUser = await _userManager.FindByEmailAsync(model.Email);
            if(newUser is not null)
                await _userManager.AddToRoleAsync(newUser, Enum.GetName(typeof(RolesEnum),0));
            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new UserNotFoundException();
        }

        public async Task<GenericResponseModel<bool>> UpdateUserAsync(UserUpdateDTO model)
        {
            GenericResponseModel<bool> response = new();
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new UserNotFoundException();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                response.Data = result.Succeeded;
                response.StatusCode = 200;
                response.Message = "Updating user successful";
                return response;
            }
            else
            {
                response.Data = result.Succeeded;
                response.StatusCode = 400;
                response.Message = "Updating user failed";
                return response;
            }
            throw new Exception("Error happened while deleting User");//todo change to custom exception
        }
    }
}
