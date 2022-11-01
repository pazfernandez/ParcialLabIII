using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class PatrimonioNet : Base
    {
        public float saldoAcreedor { get; set; }

        public PatrimonioNet(float saldoAcreedor, String nombreCuenta, int id)
        {

            this.saldoAcreedor = saldoAcreedor;
            this.nombreCuenta = nombreCuenta;
            this.id = id;
        }
    }
}
