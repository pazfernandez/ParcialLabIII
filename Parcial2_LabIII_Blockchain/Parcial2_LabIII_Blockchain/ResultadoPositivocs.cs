using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class ResultadoPositivo : Base
    {
        public float saldoAcreedor { get; set; }


        public ResultadoPositivo(float saldoAcreedor, String nombreCuenta)
        {

            this.saldoAcreedor = saldoAcreedor;
            this.nombreCuenta = nombreCuenta;
        }
    }
}
