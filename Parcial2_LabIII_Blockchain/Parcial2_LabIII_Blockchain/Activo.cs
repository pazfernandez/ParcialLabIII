using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class Activo : Base
    {
        public float debe;
        public float haber;

        public float saldoDeudor;

        public Activo(float debe, float haber, float saldoDeudor, String nombreCuenta, int id)
        {
            this.debe = debe;
            this.haber = haber;
            this.saldoDeudor = saldoDeudor;
            this.nombreCuenta = nombreCuenta;
            this.id = id;
        }
    }
}
