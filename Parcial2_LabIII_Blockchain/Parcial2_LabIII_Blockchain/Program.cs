using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Blockchain nodo1 = new Blockchain();

            nodo1.NewTransaction("Octavio", "Agustin", 25);
            nodo1.NewTransaction("Octavio", "Paz", 30);
            nodo1.NewBlock();

            nodo1.NewTransaction("Raul", "Martin", 25);
            nodo1.NewTransaction("Simon", "Ramiro", 30);
            nodo1.NewTransaction("Simon", "Fernando", 30);
            nodo1.NewBlock();

            Console.WriteLine(nodo1.Blocks[0].Transactions[0].Amount + Environment.NewLine +
                              nodo1.Blocks[1].Transactions[2].Receiver + Environment.NewLine +
                              nodo1.Blocks[0].Hash + Environment.NewLine +
                              nodo1.Blocks[1].PreviousHash + Environment.NewLine );

        }
    }
}
