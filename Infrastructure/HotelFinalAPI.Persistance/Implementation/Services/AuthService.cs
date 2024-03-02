using HotelFinalAPI.Application.Abstraction.Services.Infrastructure.TokenServices;
using HotelFinalAPI.Application.Abstraction.Services.Persistance;
using HotelFinalAPI.Application.DTOs;
using HotelFinalAPI.Application.Exceptions;
using HotelFinalAPI.Application.Models.ResponseModels;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Persistance.Implementation.Services
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }
        public async Task<GenericResponseModel<TokenDTO>> Login(string usernameOrEmail, string password, int accessTokenLifeTime)
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
            else
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (result.Succeeded)
                {
                    TokenDTO token = await _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime);
                    return new()
                    {
                        Data = token,
                        StatusCode = 200,
                        Message = "Successfull"
                    };
                }
               /* else
                {
                    return new()
                    {
                        Data = null,
                        StatusCode = 401,
                        Message = "Kimlik dogrulanmasi zamani hata olusdu"
                    };
                }*/
            }
            throw new AuthenticationErrorException();
        }


    }
}
