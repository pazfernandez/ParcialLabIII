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

        public void NewCuenta(string nombre, float monto, int tipo, int debeOHaber)
        {
            Cuenta newCuenta = new Cuenta(nombre, tipo, monto, debeOHaber);

            TempCuentas.Add(newCuenta);
        }

        public void NewBlock(String fecha, String descripcion)
        {

            string previousHash = string.Empty;
            if (Blocks.Count > 0)
            {
                previousHash = Blocks[Blocks.Count - 1].Hash;
            }

            Block newBlock = new Block(Blocks.Count, TempCuentas, previousHash, descripcion, fecha);
            newBlock.MineBlock(Difficulty);
            Blocks.Add(newBlock);

            TempCuentas = new List<Cuenta>();

        }

    }
}
