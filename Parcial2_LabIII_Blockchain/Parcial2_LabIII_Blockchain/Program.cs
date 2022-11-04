using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                //Si no es nulo llama al metodo para imprimir el libro diario
                imprimirLibroDiario(Nodo);
            }

            Console.ReadKey();
        }




        //Usuario ingresa los datos que seran guardados en el blockchain
        public Blockchain InteraccionConUsuario(Blockchain Nodo)
        {


            

            bool nuevo = true;
            do
            {


                Console.WriteLine("¿Añadir nueva entrada al libro diario? Y/N");
                string yn;
                try
                {
                    do
                    {

                        yn = Console.ReadLine().ToLower().Trim();


                        if(yn == "y")
                        {
                            nuevo = true;
                            yn = null;

                        }else if(yn == "n")
                        {
                            nuevo= false;
                            yn = null;
                        }

                    } while (yn != null);

                }catch(Exception e)
                {
                    Console.WriteLine("Ingreso erroneo. Y/N");

                }
                

                if (nuevo)
                {

                    crearEntradaLibroDiario(Nodo);
                   
                }

            } while (nuevo);

            return Nodo;
        }







        public void imprimirLibroDiario(Blockchain Nodo)
        {

            

            //Pasa por todas las entradas del libro (bloques del blockchain)
            for (int i = 0; i < Nodo.Blocks.Count; i++)
            {
                Console.WriteLine("-------------------------------------"+(i+1)+"---------------------------------------------");
                //Imprime la fecha
                Console.Write("| "+Nodo.Blocks[i].Fecha+" ");

                //Pasa por todas las cuentas que componen cada accion contable
                for (int j = 0; j < Nodo.Blocks[i].Cuentas.Length; j++)
                {   if(j != 0)
                    {
                        Console.Write("|            ");
                    }
                //Imprime el nombre de la cuenta
                    Console.Write("|" + Nodo.Blocks[i].Cuentas[j].nombre+"                      ");
                    //Imprime el debe
                    Console.Write("| " + Nodo.Blocks[i].Cuentas[j].debe+"                ");
                    //Imprime el haber
                    Console.WriteLine("| " + Nodo.Blocks[i].Cuentas[j].haber + "                |");
                }
                Console.WriteLine("|            |                                                                     |");
                //Imprime la descripcion
                Console.WriteLine("|            |      " + Nodo.Blocks[i].Descripcion + "              |");
            }
        }

        public void crearEntradaLibroDiario(Blockchain Nodo)
        {
            //Iran en el bloque junto con la lista de cuentas
            string descripcion;
            string fecha;


            //Cuentas que habra dentro de un mismo bloque
            List<Cuenta> cuentasBloque = new List<Cuenta>();

            //Todos menos debeOHaber y fecha seran guardados en un objeto de tipo cuenta para ser agregados a un bloque

            bool cont;
            bool primero = true;

            Console.WriteLine("Ingrese la fecha de la acción contable: xx/xx/xxxx");

                    //Lee y valida que la fecha sea correcta
                    do
                    {
                        fecha = Console.ReadLine();
                    } while (!DateTime.TryParse(fecha, out DateTime theDate));

                    //Ingresar una cuenta nueva
                    do
                    {
                        cont = CrearEntradaCuenta(Nodo, primero, fecha);
                        primero = false;
                    } while (cont);

                    Console.WriteLine("Ingrese la descripción de la acción contable: ");
                    do
                    {
                        descripcion = Console.ReadLine();

                    } while (descripcion == null);

                    //Creacion del nuevo bloque (entrada de libro diario) con su fecha y descripcion
                    Nodo.NewBlock(fecha, descripcion);
        }

        public bool CrearEntradaCuenta(Blockchain Nodo, bool primero, string fecha)
        {
            int tipo;
            string nombre;
            int debeOHaber;
            float monto;


            Console.WriteLine();
            Console.WriteLine("¿Quiere ingresar un cambio en una cuenta existente o nueva?");
            Console.WriteLine("0_No   1_Activo   2_Pasivo   3_Patrimonio Neto   4_Resultado Negativo   5_Resultado Positivo");

            try
            {
                do
                {
                    //Hay que agregar excepcion por si ingresan algo que no es un numero, asi no se rompe el programa
                    tipo = Convert.ToInt32(Console.ReadLine());
                } while (tipo > 5 || tipo < 0 || tipo == null || (tipo == 0 && primero));

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

                    
                    Nodo.NewCuenta(nombre, monto, tipo, debeOHaber, fecha);


                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Ingreso incorrecto. Intente de nuevo.");
                CrearEntradaCuenta(Nodo, primero, fecha);
                return false;
            }
            



            return true;
        }

        static void Main(string[] args)
        {
            string yn;

            Program programa = new Program();
            SerializarArchivo serializar = new SerializarArchivo();

            //Creacion del blockchain
            Blockchain Nodo = serializar.LeerCadena();

            Nodo.verificarHash(Nodo);

            Nodo = programa.InteraccionConUsuario(Nodo);
            programa.Correr(Nodo);

            Mayor mayor = new Mayor();
            
            mayor.registrarMayores(Nodo);

            do
            {
                Console.WriteLine("\n¿Quiere revisar algun Mayor especifico?  Y/N");
                yn = Console.ReadLine().Trim().ToLower();

                if (yn.Equals("y"))
                {
                    mayor.mostrarMayorElegido();
                }

            } while (!(yn.Equals("n")));

            

            mayor.mostrarMayores();

        }
    }
}
