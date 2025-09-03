using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LoginClient
    {
        //Esta clase representa un cliente que inicia sesión en el sistema.
        public string Email { get; set; }
        public string Password { get; set; }

        //Constructor que inicializa los atributos del cliente.
        public LoginClient(string email, string password) 
        { 
            Email = email;
            Password = password;
        }

        //Constructor para la deserializacion.
        public LoginClient() { }
    }
}
