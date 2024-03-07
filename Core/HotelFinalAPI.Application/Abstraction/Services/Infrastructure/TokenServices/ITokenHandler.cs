using HotelFinalAPI.Application.DTOs;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Application.Abstraction.Services.Infrastructure.TokenServices
{
    public interface ITokenHandler
    {
        Task<TokenDTO> CreateAccessTokenAsync(int minute,AppUser user);
        string CreateRefreshToken();
    }
}
