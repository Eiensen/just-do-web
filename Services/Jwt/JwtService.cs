using JustDo_Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JustDo_Web.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly SymmetricSecurityKey _key;

        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                    (
                        configuration["Jwt:Key"]
                    ));

            this._configuration = configuration;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id)
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Issuer"],
                Expires = DateTime.UtcNow.AddMonths(6),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);  
            
            return tokenHandler.WriteToken(token);
        }
    }
}
