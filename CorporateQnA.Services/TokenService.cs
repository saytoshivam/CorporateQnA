using CorporateQnA.Services.Data;
using CorporateQnA.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CorporateQnA.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey Key;
        private int UserId;
        public TokenService(IConfiguration config)
        {
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(ApplicationUser user)
        {
            UserId = user.Id;
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.NameId,user.UserName),
               new Claim("UserID",user.Id.ToString())
            };
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }
        public int GetLoggedInUserId()
        {
            return UserId;
        }
    }
}
