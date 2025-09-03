using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TokenClientService : ItokenClient
    {
        public readonly IConfiguration _confi;

        public TokenClientService(IConfiguration configuration)
        {
            _confi = configuration;
        }

        public string CreateToken(Client client)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.Name , client.Name),
                new Claim(ClaimTypes.Email, client.Email),
                new Claim("Id", client.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confi["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _confi["Jwt:Issuer"],
                audience: _confi["Jwt:Audience"],
                claims: claim,
                expires: DateTime.Now.AddMinutes(4),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
