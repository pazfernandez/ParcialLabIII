using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class Transaction
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        //quien hace la transaccion
        public string Sender { get; set; }

        //quien recibe la transaccion
        public string Receiver { get; set; }

        //Monto de la transaccion
        public decimal Amount { get; set; }


        //En lugar de que aparezca el texto de la transaccion, se genera un hash, por cada transaccion un hash
        public string Hash { get { return HashHelper.CalculateHash(string.Format("{0}{1}{2}", Sender, Receiver, Amount)); } }


        public Transaction(string sender, string receiver, decimal amount)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
            Timestamp = DateTime.Now;
        }

    }
}
