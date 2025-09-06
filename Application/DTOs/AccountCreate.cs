using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    public class AccountCreate
    {
        public int idClient { get; set; }
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }

    }
}
