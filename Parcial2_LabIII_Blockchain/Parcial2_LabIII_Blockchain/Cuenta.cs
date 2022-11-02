using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    [Serializable]
    internal class Cuenta
    {
        public String nombre { get; set; }
        public float debe { get; set; }
        public float haber { get; set; }
        public int tipo { get; set; }

        //En lugar de que aparezca el texto de la transaccion, se genera un hash, por cada transaccion un hash
        public string Hash { get { return HashHelper.CalculateHash(string.Format("{0}{1}{2}{3}", nombre, tipo, debe, haber)); } }

        public Cuenta(string nombre, int tipo, float monto, int debeOHaber)
        {
            this.nombre = nombre;
            this.tipo = tipo;

            if(debeOHaber == 0)
            {
                this.debe = monto;
            }
            else
            {
                this.haber =monto;
            }
            
        }
    }
}
