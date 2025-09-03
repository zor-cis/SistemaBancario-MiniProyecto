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
        public string TypeAccount { get; private set; }
        public string HolderAccount { get; set; }
        public Client Client { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isActive { get; set; }

        public Account(string accountNumber, string typeAccount, string holderAccount, Client client)
        {
            //Validamos que parametros importantes no puedan ser nulos o negativos.

            if (string.IsNullOrEmpty(holderAccount))
                throw new ArgumentException("La cuenta debe tener un titular");
            if(client == null)
                throw new ArgumentNullException(nameof(client), "Cliente nulo");

            if (string.IsNullOrEmpty(accountNumber))
                throw new ArgumentException("El numero de la cuenta no puede estar vacio");

            if(Balance < 0)
                throw new ArgumentException("El saldo inicial no puede ser negativo");

            AccountNumber = accountNumber;
            TypeAccount = typeAccount;
            HolderAccount = holderAccount;
            Client = client;
            Balance = 0;
            CreatedAt = DateTime.Now;
            isActive = true;
        }

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