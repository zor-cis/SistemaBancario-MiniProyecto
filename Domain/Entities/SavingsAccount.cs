using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Esta clase hereda de Account y representa una cuenta de ahorros, con un limite de retiro del 50% del saldo disponible, y con la capacidad de realizar depositos de cualquier cantidad. 

namespace Domain.Entities
{
    public class SavingsAccount : Account
    {
        //Establecemos el limite de retiros, que en este caso seria de un 50%.
        public decimal limit { get; private set; } = 0.50m;

        public SavingsAccount(string accountNumber, string holderAccount, Client client) : base(accountNumber, "Cuenta de Ahorros", holderAccount, client)
        {
        }

        //Indicamos que en la cuenta de ahorro el retiro no puede exceder el 50% del saldo disponible
        public override void Withdraw(decimal amount)
        {
            if(amount <= 0)
                throw new ArgumentException("El monto debe ser mayor a cero");
            if(amount > Balance)
                throw new InvalidOperationException("Monto que desea retirar es mayor al saldo disponible");
            if(amount > Balance * limit)
                throw new InvalidOperationException("El monto excede el limite permitio para retiros");

            Balance -= amount;
        }
    }
}
