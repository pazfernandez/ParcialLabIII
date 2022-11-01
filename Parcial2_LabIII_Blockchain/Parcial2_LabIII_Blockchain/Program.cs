using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Parcial2_LabIII_Blockchain
{
    internal class Program
    {

        private Blockchain Nodo;
        private SerializarArchivo SerializoArchivo;
        //private List<Blockchain> ListaNodos;

        public Program()
        {
            SerializoArchivo = new SerializarArchivo();
            //ListaNodos = new List<Blockchain>();
        }

        public void Correr()
        {
            Nodo = new Blockchain();

            //Operaciones

            Nodo.NewTransaction("Octavio", "Agustin", 25);
            Nodo.NewTransaction("Octavio", "Paz", 30);
            Nodo.NewBlock();

            Nodo.NewTransaction("Raul", "Martin", 25);
            Nodo.NewTransaction("Simon", "Ramiro", 30);
            Nodo.NewTransaction("Simon", "Fernando", 30);
            Nodo.NewBlock();

            //Fin operaciones

            SerializoArchivo.GuardarCadena(Nodo);

            Nodo = SerializoArchivo.LeerCadena();
            if (Nodo != null)
            {
                Console.WriteLine(/*Imprimo algo de la blockchain*/);
                Console.WriteLine(Nodo.Blocks[0].Transactions[0].Amount + Environment.NewLine +
                              Nodo.Blocks[1].Transactions[2].Receiver + Environment.NewLine +
                              Nodo.Blocks[0].Hash + Environment.NewLine +
                              Nodo.Blocks[1].PreviousHash + Environment.NewLine);
            }

            Console.ReadKey();
        }

        //Usuario ingresa los datos que seran guardados en el blockchain
        public void InteraccionConUsuario()
        {
            //Iran en el bloque junto con la lista de cuentas
            String descripcion;
            String fecha;


            //Cuentas que habra dentro de un mismo bloque
            List<Cuenta> cuentasBloque = new List<Cuenta>();

            //Todos menos debeOHaber y fecha seran guardados en un objeto de tipo cuenta para ser agregados a un bloque
            
            int tipo;
            String nombre;
            int debeOHaber;
            float monto;


            Console.WriteLine("Añadir entrada al libro diario: ");
            Console.WriteLine("Ingrese la fecha de la acción contable: xx/xx/xxxx");

            //Lee y valida que la fecha sea correcta
            do
            {
                fecha = Console.ReadLine();
            }while (!FormatoCorrecto(fecha));

            Console.WriteLine("¿Quiere ingresar un cambio en una cuenta existente o nueva?");
            Console.WriteLine("0_No   1_Activo   2_Pasivo   3_Patrimonio Neto   4_Resultado Negativo   5_Resultado Positivo");

            do
            {
                //Hay que agregar excepcion por si ingresan algo que no es un numero, asi no se rompe el programa
                tipo = Convert.ToInt32(Console.ReadLine());
            } while (tipo > 5 || tipo < 0 || tipo == null);

            Console.WriteLine("Ingrese el nombre de la cuenta: ");
            do
            {
                nombre = Console.ReadLine();
            } while (nombre == null);

            //Si la cuenta es de tipo activo o pasivo, se elige entre el debe o el haber
            if(tipo == 1 || tipo == 2)
            {
                Console.WriteLine("¿Quiere ingresar un monto en el haber o en el debe?");
                Console.WriteLine("0_Debe   1_Haber");

                do
                {
                    //Hay que agregar excepcion por si ingresan algo que no es un numero, asi no se rompe el programa
                    debeOHaber = Convert.ToInt32(Console.ReadLine());
                } while (debeOHaber != 0 || debeOHaber != 1 || tipo == null);
            }else if(tipo == 4)
            {
                debeOHaber = 0; //Si es una perdida solo deja poner un monto en el debe
            }
            else //Si es PN o una ganancia, solo deja poner un monto en el haber
            {
                debeOHaber = 1;
            }

            //Ingresar un monto
            do
            {
                //Hay que agregar excepcion por si ingresan algo que no es un numero, asi no se rompe el programa
                monto = (float) Convert.ToDouble(Console.ReadLine());
            } while (monto == null);

            //Cuenta cuentaNueva = new Cuenta(nombre, )
            

        }

        //Metodo para validar el formato de la fecha pasada por el usuario
        public bool FormatoCorrecto(String fecha)
        {

            if(fecha == null)
            {
                return false;
            }else if (DateTime.TryParse(fecha, out DateTime theDate)) {
                return true;
            }
            else
            {
                return false;
            }
            

        }

        /*public void CorrerConLista()
        {
            Nodo = new Blockchain();

            //Operaciones

            //Fin operaciones

            ListaNodos.Add(Nodo);

            SerializoArchivo.GuardarListaCadena(ListaNodos);

            ListaNodos = SerializoArchivo.LeerListaCadena();
            if (ListaNodos != null)
            {
                for (int i = 0; i < ListaNodos.Count; i++)
                {
                    Nodo = ListaNodos[i];
                    Console.WriteLine(Imprimo algo de la blockchain);
                }

            }

            Console.ReadKey();
        }*/

        static void Main(string[] args)
        {

            Program programa = new Program();
            programa.Correr();

            //Blockchain nodo1 = new Blockchain();

            /*nodo1.NewTransaction("Octavio", "Agustin", 25);
            nodo1.NewTransaction("Octavio", "Paz", 30);
            nodo1.NewBlock();

            nodo1.NewTransaction("Raul", "Martin", 25);
            nodo1.NewTransaction("Simon", "Ramiro", 30);
            nodo1.NewTransaction("Simon", "Fernando", 30);
            nodo1.NewBlock();

            Console.WriteLine(nodo1.Blocks[0].Transactions[0].Amount + Environment.NewLine +
                              nodo1.Blocks[1].Transactions[2].Receiver + Environment.NewLine +
                              nodo1.Blocks[0].Hash + Environment.NewLine +
                              nodo1.Blocks[1].PreviousHash + Environment.NewLine );*/
            programa.InteraccionConUsuario();
        }
    }
}
