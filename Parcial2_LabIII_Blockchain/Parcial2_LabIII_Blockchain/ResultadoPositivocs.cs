using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class ResultadoPositivo : Base
    {
        public float saldoDeudor { get; set; }


        public ResultadoPositivo(float saldoDeudor, String nombreCuenta, int id)
        {

            this.saldoDeudor = saldoDeudor;
            this.nombreCuenta = nombreCuenta;
            this.id = id;
        }
    }
}
