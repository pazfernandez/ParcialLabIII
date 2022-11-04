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
        public string fecha { get; set; }
        public string nombre { get; set; }
        public float debe { get; set; }
        public float haber { get; set; }
        public int tipo { get; set; }

        //En lugar de que aparezca el texto de la transaccion, se genera un hash, por cada transaccion un hash
        public string Hash { get { return HashHelper.CalculateHash(string.Format("{0}{1}{2}{3}{4}", nombre, tipo, debe, haber, fecha)); } }

        public Cuenta(string nombre, int tipo, float monto, int debeOHaber, string fecha)
        {
            this.fecha = fecha;
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
