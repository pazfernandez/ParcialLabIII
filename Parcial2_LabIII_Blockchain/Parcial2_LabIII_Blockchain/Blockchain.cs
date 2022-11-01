using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    [Serializable]
    internal class Blockchain
    {
        public List<Block> Blocks { get; set; }

        public List<Cuenta> TempCuentas { get; set; }

        public const int Difficulty = 4;


        public Blockchain()
        {

            Blocks = new List<Block>();
            TempCuentas = new List<Cuenta>();
        }

        public void NewCuenta(string nombre, float debeOHaber, int tipo)
        {

            Cuenta newCuenta = new Cuenta(nombre, debeOHaber, tipo);

            TempCuentas.Add(newCuenta);
        }

        public void NewBlock()
        {

            string previousHash = string.Empty;
            if (Blocks.Count > 0)
            {
                previousHash = Blocks[Blocks.Count - 1].Hash;
            }

            Block newBlock = new Block(Blocks.Count, TempCuentas, previousHash);
            newBlock.MineBlock(Difficulty);
            Blocks.Add(newBlock);

            TempCuentas = new List<Cuenta>();

        }

    }
}
