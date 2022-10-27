using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class Block
    {

        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        public Transaction[] Transactions { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }


        public int proof { get; set; }


        public Block(int index, List<Transaction> transactions, string previousHash)
        {

            Id = index;
            Transactions = transactions != null ? transactions.ToArray() : new Transaction[0];
            Timestamp = DateTime.Now;
            PreviousHash = previousHash;

        }

        #region Mining

        //Crea el hash del bloque y le asigna 4 ceros al principio
        private bool HashIsValid(string text, int difficulty)
        {                  //difficulty --> Numero de 0 por el que tiene que empezar el hash, si no empieza por ese numero de 0, no es valido

            string hash = HashHelper.CalculateHash(text);
            string zeros = string.Empty.PadLeft(difficulty, '0');               //Nos crea un string con "difficulty" ceros

            return hash.StartsWith(zeros);

        }


        public int MineBlock(int difficulty)
        {

            //string initialText = string.Format("{0}{1}", Id, Timestamp);
            string initialText = string.Format("{0}{1}{2}", Id, Timestamp, Transactions.Select(t => t.Hash).Aggregate((i, j) => i + j));      //Con esta funcion se concatenan todos los Hash dentro del bloque
            proof = 0;
            string text = string.Format("{0}{1}", initialText, proof);


            while (!HashIsValid(text, difficulty))
            {

                proof++;
                text = string.Format("{0}{1}", initialText, proof);
            }

            Hash = HashHelper.CalculateHash(text);

            return proof;               //no es necesario este retorno

        }

        #endregion Mining

    }
}
