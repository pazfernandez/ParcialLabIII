using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    [Serializable]
    internal class Blockchain
    {
        public List<Block> Blocks { get; set; }

        public List<Transaction> TempTransactions { get; set; }

        public const int Difficulty = 4;


        public Blockchain()
        {

            Blocks = new List<Block>();
            TempTransactions = new List<Transaction>();
        }

        public void NewTransaction(string sender, string receiver, decimal amount)
        {

            Transaction newTransaction = new Transaction(sender, receiver, amount);

            TempTransactions.Add(newTransaction);
        }

        public void NewBlock()
        {

            string previousHash = string.Empty;
            if (Blocks.Count > 0)
            {
                previousHash = Blocks[Blocks.Count - 1].Hash;
            }

            Block newBlock = new Block(Blocks.Count, TempTransactions, previousHash);
            newBlock.MineBlock(Difficulty);
            Blocks.Add(newBlock);

            TempTransactions = new List<Transaction>();

        }

    }
}
