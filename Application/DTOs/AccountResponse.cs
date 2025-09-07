using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AccountResponse
    {
        public string AccountNumber { get; set; }
        public int TypeAccount { get; set; }
        public string HolderAccount { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isActive { get; set; }
    }
}
