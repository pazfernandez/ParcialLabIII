using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class Pasivo : Base
    {
        public float debe;
        public float haber;

        public float saldoAcreedor;


        public Pasivo(float debe, float haber, float saldoAcreedor, String nombreCuenta, int id)
        {
            this.debe = debe;
            this.haber = haber;
            this.saldoAcreedor = saldoAcreedor;
            this.nombreCuenta = nombreCuenta;
            this.id = id;
        }
    }
}
