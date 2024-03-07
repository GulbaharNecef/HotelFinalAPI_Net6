using HotelFinalAPI.Application.Abstraction.Services.Infrastructure.TokenServices;
using HotelFinalAPI.Application.DTOs;
using HotelFinalAPI.Domain.Entities.IdentityEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinalAPI.Infrastructure.Implementation.Services.TokenServices
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TokenDTO> CreateAccessTokenAsync(int minute,AppUser user)
        {
            TokenDTO token = new();
            //Security Key'in simmetrigini aliriq
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            //Sifrelenmis kimligi olusturuyoruz
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Olusturulacak token ayarlarini veriyoruz
            token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                signingCredentials: signingCredentials,
                expires: token.Expiration,
                notBefore: DateTime.UtcNow, //Token uretildiyi andan ne qeder sonra devreye girsin?now .AddMinutes(1) desem token 1 deqiqe sonra devreye girer, tokenin timesinden cixilir ama
                claims: new List<Claim> { new(ClaimTypes.Name,user.UserName)}
                );

            //Token olusturucu sinifindan bir ornek alalim
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();

            return token;

        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
