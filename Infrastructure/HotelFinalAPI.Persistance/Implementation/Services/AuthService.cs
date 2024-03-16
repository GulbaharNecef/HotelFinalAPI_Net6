using HotelFinalAPI.Application.Abstraction.Services.Infrastructure.TokenServices;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs;
using HotelFinalAPI.Application.Exceptions;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace HotelFinalAPI.Persistance.Implementation.Services
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IUserService userService, IConfiguration configuration, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _configuration = configuration;
        }
        public async Task<GenericResponseModel<TokenDTO>> Login(string usernameOrEmail, string password/*, int accessTokenLifeTime*/)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            }
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                TokenDTO token = await _tokenHandler.CreateAccessTokenAsync(Int32.Parse(_configuration["Token:AccessTokenLifeTimeInMinutes"]), user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, Int32.Parse(_configuration["Token:AddOnAccessTokenDate"]));
                return new()
                {
                    Data = token,
                    StatusCode = 200,
                    Message = "Successfull"
                };
            }
            throw new AuthenticationErrorException();
        }

        public async Task<GenericResponseModel<TokenDTO>> RefreshTokenLoginAsync([FromQuery] string refreshToken)
        {
            GenericResponseModel<TokenDTO> response = new();
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                TokenDTO token = await _tokenHandler.CreateAccessTokenAsync(Int32.Parse(_configuration["Token:AccessTokenLifeTimeInMinutes"]), user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, Int32.Parse(_configuration["Token:AddOnAccessTokenDate"]));
                response.Data = token;
                response.StatusCode = 200;
                response.Message = "Login with refresh token was successfull";
                return response;
            }
            else
                throw new UserNotFoundException();
        }

        public async Task<GenericResponseModel<bool>> Logout(string usernameOrEmail)
        {
            GenericResponseModel<bool> response = new();
            var user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user is null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user is not null)
            {
                user.RefreshToken = null;
                user.RefreshTokenEndDate = null;
                IdentityResult result = await _userManager.UpdateAsync(user);
                await _signInManager.SignOutAsync();
                if (result.Succeeded)
                {
                    response.Data = result.Succeeded;
                    response.StatusCode = 200;
                    response.Message = "Logout successful!";
                    return response;
                }
                else
                {
                    response.Data = result.Succeeded;
                    response.StatusCode = 400;
                    response.Message = "Logout failed";
                    return response;
                }
            }
            throw new AuthenticationErrorException();
        }

        public async Task<GenericResponseModel<bool>> ResetPasswordAsync(string email, string currentPass, string newPass)
        {
            GenericResponseModel<bool> response = new() { };
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UserNotFoundException();
            IdentityResult result = await _userManager.ChangePasswordAsync(user, currentPass, newPass);
            if (result.Succeeded)
            {
                response.Data = result.Succeeded;
                response.StatusCode = 200;
                response.Message = "Password change successful";
                return response;
            }
            else
            {
                response.Data = result.Succeeded;
                response.StatusCode = 400;
                response.Message = "Password change failed";
                return response;
            }
            throw new AuthenticationErrorException();
        }
    }
}
