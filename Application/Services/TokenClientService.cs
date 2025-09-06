using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

//Esta clase se encarga de generar el token para el cliente que entra en el sistema. 
//JWT: Token  que contiene informacion codificada en json, firmada digitalmente para que no pueda editarse 
namespace Application.Services
{
    public class TokenClientService : ItokenClient
    {
        //Interfaz de asp.net que representa la configuracion de la aplicacion.
        //permite llamar la configuracion de appseting al codigo.

        public readonly IConfiguration _confi; 

        public TokenClientService(IConfiguration configuration)
        {
            _confi = configuration;
        }

        //Metodo que crea el token para el cliente que entra al sistema. Crea un jwt con
        //la informacion del cliente.
        public string CreateToken(Client client)
        {
            //Se crea un arreglo Claim, informacion del usuario que se guardara en el token.
            var claim = new[]
            {
                new Claim(ClaimTypes.Name , client.Name),
                new Claim(ClaimTypes.Email, client.Email),
                new Claim("Id", client.Id.ToString())
            };

            //Toma la clave del appseting, la convierte en arreglo de bytes y crea una llave de
            //seguridad para firmar el token. 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confi["Jwt:Key"]));

            //Crea un objeto SigningCredentials (credenciales de firma) que contiene la llave
            //y el algoritmo de seguridad para firmar el token.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Se crea un token y se indica quien lo creo y para quien es (issuer y audience),se
            //toma la informacion del usuario de los claims, se indica el tiempo de expiracion y
            //se firma con el creds.
            var token = new JwtSecurityToken(
                issuer: _confi["Jwt:Issuer"],
                audience: _confi["Jwt:Audience"],
                claims: claim,
                expires: DateTime.Now.AddMinutes(4),
                signingCredentials: creds
            );

            //Se crea el manejo del token(se encargara de manejar, leer y validar) y lo convierte a
            //string para devolverlo.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
