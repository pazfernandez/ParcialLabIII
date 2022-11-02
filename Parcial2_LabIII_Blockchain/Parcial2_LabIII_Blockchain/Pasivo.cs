using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class Pasivo : Base
    {
        public float debe { get; set; }
        public float haber { get; set; }
        public float saldoAcreedor { get; set; }


        public Pasivo(float debe, float haber, String nombreCuenta)
        {
            this.debe = debe;
            this.haber = haber;
            this.nombreCuenta = nombreCuenta;
        }
    }
}
