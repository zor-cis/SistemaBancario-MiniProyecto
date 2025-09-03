using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthClient
    {
        Task<AuthClient> Login(LoginClient dto);
        Task<AuthClient> SignUp(SignUpClient dto);
    }
}
