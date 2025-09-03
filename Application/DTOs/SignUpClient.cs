using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    //Esta clase representa un cliente que se registra en el sistema.
    public class SignUpClient
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }

        //Constructor que inicializa los atributos del cliente y valida que los datos sean correctos.
        public SignUpClient() { }


        //El metodo establece la edad del cliente en base a su fecha de nacimiento.
        public int Age() 
        { 
          int today = DateTime.Today.Year;
          int birthYear = DateOfBirth.Year;
          int age = today - birthYear;
          if (DateTime.Today < DateOfBirth.AddYears(age))
          {
              age--;
          }

            return age;
        }

        public string FormatDate
        {
            get { return DateOfBirth.ToString("dd-MM-yyyy"); }
        }
    }
}
