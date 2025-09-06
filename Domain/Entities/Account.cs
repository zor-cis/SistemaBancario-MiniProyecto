using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Esta clase abstracta representa una cuenta generica bancaria, con atributos generales y metodos para establecer acciones de retiro, deposito y consulta de balance.

namespace Domain.Entities
{
    public abstract class Account
    {
        public string AccountNumber { get; private set; }
        public AccountType TypeAccount { get; private set; }
        public string HolderAccount { get; set; }

        public int IdClient { get; set; }
        public Client Client { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isActive { get; set; }

        public Account(string accountNumber, AccountType typeAccount, string holderAccount, Client client, int idClient)
        {

            AccountNumber = accountNumber;
            TypeAccount = typeAccount;
            HolderAccount = holderAccount;
            Client = client;
            Balance = 0;
            CreatedAt = DateTime.Now;
            isActive = true;
            IdClient = idClient;

        }

        public Account() { }

        public abstract void Withdraw(decimal amount);

        //Indicamos que para realizar un deposito el monto debe ser mayor a cero.
        public virtual void Deposit(decimal amount) 
        { 
            if(amount <= 0)
                throw new ArgumentException("El monto debe ser mayor a cero");
            Balance += amount;
        }
        public virtual decimal GetBalance()
        {
            return Balance;
        }
    }
}