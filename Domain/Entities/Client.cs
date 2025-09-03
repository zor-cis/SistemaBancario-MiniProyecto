using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Esta clase representa un cliente del sistema bancario, con atributos generales para las futuras operaciones.

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }

        public Client(int id, string name, string lastName, int documentTypeId, string email, long phoneNumber, int age, DateTime dateOfBirth, string password)
        {

            Id = id;
            Name = name;
            LastName = lastName;
            DocumentTypeId = documentTypeId;
            Email = email;
            PhoneNumber = phoneNumber;
            Age = age;
            DateOfBirth = dateOfBirth;
            Password = password;
        }

        public Client() { }

        //Metodo para obtener el nombre completo del cliente
        public string FullName()
        {
            return $"{Name} {LastName}";
        }
    }
}
