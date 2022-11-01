using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class Cuenta
    {
        public String nombre;
        public float debe;
        public float haber;
        public int tipo;


        public Cuenta(string nombre, float haber, int tipo)
        {
            this.nombre = nombre;
            this.haber = haber;
            this.tipo = tipo;
        }
        public Cuenta(string nombre, int tipo, float debe)
        {
            this.nombre = nombre;
            this.debe = debe;
            this.tipo = tipo;
        }
    }
}
