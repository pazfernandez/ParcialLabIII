using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class ResultadoNegativo : Base
    {
        public float saldoDeudor { get; set; }

        public ResultadoNegativo(float saldoDeudor, String nombreCuenta)
        {

            this.saldoDeudor = saldoDeudor;
            this.nombreCuenta = nombreCuenta;
        }
    }
}
