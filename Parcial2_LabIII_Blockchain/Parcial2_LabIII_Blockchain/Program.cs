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

        public void Correr(Blockchain Nodo)
        {
            

            SerializoArchivo.GuardarCadena(Nodo);

            Nodo = SerializoArchivo.LeerCadena();
            if (Nodo != null)
            {
                Console.WriteLine(/*Imprimo algo de la blockchain*/);
                /*Console.WriteLine(Nodo.Blocks[0].Cuentas[0].monto + Environment.NewLine +
                              Nodo.Blocks[1].Cuentas[2].nombre + Environment.NewLine +
                              Nodo.Blocks[0].Hash + Environment.NewLine +
                              Nodo.Blocks[1].PreviousHash + Environment.NewLine);*/
                Console.WriteLine(Nodo.Blocks[0].Cuentas[0].debe);
            }

            Console.ReadKey();
        }

        //Usuario ingresa los datos que seran guardados en el blockchain
        public Blockchain InteraccionConUsuario()
        {
            //Creacion del blockchain
            Nodo = new Blockchain();


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

            //Ingresar una cuenta nueva
            do
            {


                Console.WriteLine("¿Quiere ingresar un cambio en una cuenta existente o nueva?");
                Console.WriteLine("0_No   1_Activo   2_Pasivo   3_Patrimonio Neto   4_Resultado Negativo   5_Resultado Positivo");


                do
                {
                    //Hay que agregar excepcion por si ingresan algo que no es un numero, asi no se rompe el programa
                    tipo = Convert.ToInt32(Console.ReadLine());
                } while (tipo > 5 || tipo < 0 || tipo == null);

                //Si se quiere seguir agregando cuentas
                if (tipo != 0)
                {
                    Console.WriteLine("Ingrese el nombre de la cuenta: ");
                    do
                    {
                        nombre = Console.ReadLine();
                    } while (nombre == null);

                    //Si la cuenta es de tipo activo o pasivo, se elige entre el debe o el haber
                    if (tipo == 1 || tipo == 2)
                    {
                        Console.WriteLine("¿Quiere ingresar un monto en el haber o en el debe?");
                        Console.WriteLine("0_Debe   1_Haber");

                        do
                        {
                            //Hay que agregar excepcion por si ingresan algo que no es un numero, asi no se rompe el programa
                            debeOHaber = Convert.ToInt32(Console.ReadLine());
                        } while (debeOHaber != 0 && debeOHaber != 1);
                    }
                    else if (tipo == 4)
                    {
                        debeOHaber = 0; //Si es una perdida solo deja poner un monto en el debe
                    }
                    else //Si es PN o una ganancia, solo deja poner un monto en el haber
                    {
                        debeOHaber = 1;
                    }

                     Console.WriteLine("Ingrese un monto: ");
                    //Ingresar un monto
                    do
                    {
                        //Hay que agregar excepcion por si ingresan algo que no es un numero, asi no se rompe el programa
                        monto = (float)Convert.ToDouble(Console.ReadLine());
                    } while (monto == null);

                    Nodo.NewCuenta(nombre, monto, tipo, debeOHaber);

                    
                }



            } while (tipo != 0);
            
           Console.WriteLine("Ingrese la descripción de la acción contable: ");
            do
            {
                descripcion = Console.ReadLine();

            }while(descripcion == null);

            //Creacion del nuevo bloque (entrada de libro diario) con su fecha y descripcion
            Nodo.NewBlock(fecha, descripcion);
            return Nodo;
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

        

        static void Main(string[] args)
        {

            Program programa = new Program();
            Blockchain nodo = programa.InteraccionConUsuario();
            programa.Correr(nodo);


            
        }
    }
}
