using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public class FactoryAccount
    {
        public static Account Create(AccountType TypeAcount, string accountNumber, int idClient, string holderAccount, Client client) 
        {
            return TypeAcount switch
            {
                AccountType.Current => new CurrentAcount(accountNumber, holderAccount, client, idClient),
                AccountType.Saving => new SavingsAccount(accountNumber, holderAccount, client, idClient),
                _ => throw new ArgumentException("Tipo de cuenta no valida"),
            };
        }
    }
}
