using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Esta clase hereda de Account y representa una cuenta corriente, sin limite de retiro , pero con un monto minimo de deposito de 1000.

namespace Domain.Entities
{
    public class CurrentAcount : Account
    {
        public CurrentAcount(string accountNumber, string holderAccount, Client client) : base(accountNumber, "Cuenta Corriente", holderAccount, client)
        {
        }

        //Establecemos un metodo de retiro sin limites pero que valide que el monto debe ser mayor a cero y no debe exceder el balance.
        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El monto debe ser mayor a cero");
            if (amount > Balance)
                throw new InvalidOperationException("Monto que desea retirar es mayor al saldo disponible");

            Balance -= amount;
        }

        //Indicamos que en la cuenta corriente el deposito debe ser mayor a 1000.
        public override void Deposit(decimal amount)
        {
            if(amount < 1000)
                throw new ArgumentException("El monto minimo para depositar en una cuenta corriente es de 1000");
            base.Deposit(amount);
        }
    }
}
