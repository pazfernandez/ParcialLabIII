using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class ResultadoNegativo : Base
    {
        public float saldoAcreedor;

        public ResultadoNegativo(float saldoAcreedor, String nombreCuenta, int id)
        {

            this.saldoAcreedor = saldoAcreedor;
            this.nombreCuenta = nombreCuenta;
            this.id = id;
        }
    }
}
