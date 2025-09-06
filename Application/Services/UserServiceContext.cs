using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

//Clase que busca los datos del usuario que este en sesion.
namespace Application.Services
{
    public class UserServiceContext : IUserContextService
    {
        //Inyectamos el HttpContext para obtener la informacion del usuario en sesion.
        private readonly IHttpContextAccessor _httpUser;

        public UserServiceContext(IHttpContextAccessor httpUser)
        {
            _httpUser = httpUser;
        }

        public CurrentUser GetCurrentUser()
        {
            //Obtenenemos el usuario y verificamos si hay usuario y si esta autenticado.
            var user = _httpUser.HttpContext?.User;
            if (user == null || !isAuthenticated())
            {
                return null;
            }

            //Buscamos los claims que creamos en el token.
            var userId = user.FindFirst("Id")?.Value;
            var userFullName = user.FindFirst(ClaimTypes.Name)?.Value;
            var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

            //Devuelve el objeto con el usuario en sesion.
            return new CurrentUser
            {
                Id = int.Parse(userId),
                FullName = userFullName,
                Email = userEmail
            };
        }

        //#Metodo auxiliar que verifica si el usuario esta autenticado.
        private bool isAuthenticated() 
        {
            //Verificamos si el usuario esta autenticado.
            return _httpUser.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
