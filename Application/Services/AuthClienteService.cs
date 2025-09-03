using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

using System;

//Esta clase es la responsabe de manejar el servicio de inicio de sesión y registro de clientes, con authenticación.

namespace Application.Services
{
    public class AuthClienteService : IAuthClient
    {
        private readonly IClientRepository _repo;
        private readonly ItokenClient _token;
        private readonly LogService _log;

        //Constructor que recibe las dependencias para el servicio.
        public AuthClienteService(IClientRepository repo, ItokenClient token, LogService log)
        {
            _repo = repo;
            _token = token;            
            _log = log;

        }

        //Este metodo maneja el inicion de sesion de un cliente, se valida si el cliente existe
        //y si la contraseña y el email es correcto y se esta ingresando de acuerdo al formato
        //esperado, se crea un token y si todo es correcto se da acceso. 
        public async Task<AuthClient> Login(LoginClient dto)
        {
            try
            {
                if (!isValidateEmail(dto.Email))
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("El correo electrónico es invalido");
                }

                if (!isValidatePassword(dto.Password))
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("La contraseña es invalida");
                }

                var client = await _repo.GetClientByEmail(dto.Email);

                if (client == null)
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("El cliente encontrado");
                }

                if (client.Password != dto.Password)
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("La contraseña es incorrecta");
                }

                var token = _token.CreateToken(client);
                await _log.LogLoginUser(dto.Email, true);

                return new AuthClient
                {
                    NombreCompleto = $"{client.FullName()}",
                    Email = client.Email,
                    Token = token,
                };
            }
            catch (Exception ex)
            {
                await _log.LogLoginUser(dto.Email, false);
                throw new InvalidOperationException("Error al iniciar sesión", ex);
            }
        }

        //Este metodo maneja el registro de un cliente, se valida que el correo y el numero de identidad no esten ya registrados, y se valida que el cliente sea mayor de edad, si todo es correcto se crea un nuevo cliente y se genera el token.
        public async Task<AuthClient> SignUp(SignUpClient dto)
        {
            try
            {
                if (!isValidateEmail(dto.Email))
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("Correo invalido");
                }

                if (!isValidatePassword(dto.Password))
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("La contraseña debe tener al menos 6 caracteres, un número y una letra mayúscula.");
                }

                if (dto.Age() < 18)
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("Debe ser mayor de edad para registrarte");
                }

               var exist_email = await _repo.GetClientByEmail(dto.Email);
               var exist_dti = await _repo.GetDocumentTypeId(dto.DocumentTypeId);

                if (exist_email != null)
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("Ya existe una cuenta con este correo");
                }

               if (exist_dti != null)
                {
                    await _log.LogLoginUser(dto.Email, false);
                    throw new ArgumentException("Este nuemero de documento ya esta registrado en el sistema.");
                }

                

                var client = new Client(
                    id: 0,
                    name: dto.Name,
                    lastName: dto.LastName,
                    documentTypeId: dto.DocumentTypeId,
                    email: dto.Email,
                    phoneNumber: dto.PhoneNumber,
                    age: dto.Age(),
                    dateOfBirth: dto.DateOfBirth,
                    password: dto.Password
                );

                await _repo.AddClient(client);
                await _log.LogLoginUser(dto.Email, true);

                var token = _token.CreateToken(client);

                return new AuthClient
                {
                    NombreCompleto = client.FullName(),
                    Email = client.Email,
                    Token = token,
                };
            }
            catch (Exception ex)
            {
                await _log.LogLoginUser(dto.Email, false);
                throw new InvalidOperationException("Error al registrarse", ex);
            }
        }





        //#METODOS AUXILIARES: Validacion de Email y Password

        //Valida que el email tenga un formato correcto.
        private bool isValidateEmail(string email) 
        { 
            return !string.IsNullOrWhiteSpace(email) &&  //verifica que no sea nulo o espacios en blanco
                email.Contains("@") && //verifica que contenga el simbolo @
                email.Contains(".") && //verifica que contenga un punto
                email.IndexOf("@") < email.LastIndexOf("."); //verifica que el @ este antes del punto
        }

        //Valida que la contraseña tenga el formato correcto.
        private bool isValidatePassword(string password) 
        { 
            return !string.IsNullOrWhiteSpace(password) && //verifica que no sea nulo o espacios en blanco
                password.Length >= 6 && //verifica que tenga al menos 6 caracteres
                password.Any(char.IsDigit) && //verifica que tenga al menos un numero
                password.Any(char.IsUpper); //verifica que tenga al menos una letra mayuscula
        }
    }
}
