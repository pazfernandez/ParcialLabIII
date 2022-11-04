using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class Mayor
    {
        public List<Activo> Activos { get; set; }
        public List<Pasivo> Pasivos { get; set; }
        public List<ResultadoNegativo> ResultadosNegativos { get; set; }
        public List<ResultadoPositivo> ResultadosPositivos { get; set; }
        public List<PatrimonioNet> PatrimoniosNetos { get; set; }

        public Mayor() { }


        public void registrarMayores(Blockchain Nodo)
        {
            //i = cada entrada del libro diario
            for (int i = 0; i < Nodo.Blocks.Count; i++)
            {
                //j = cada cuenta de cada entrada
                for (int j = 0; j < Nodo.Blocks[i].Cuentas.Length; j++)
                {
                    //Guardar el nombre de la cuenta
                    string nombreCuenta = Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim();

                    switch (Nodo.Blocks[i].Cuentas[j].tipo)
                    {

                        //Activo
                        case 1:

                            //Si la lista de activos no es nula
                            if(Activos != null)
                            {
                                

                                //Pasar por cada activo en la lista
                                for (int a = 0; a < Activos.Count; a++)
                                {
                                    //Comparar nombre de la Cuenta con los nombres de los activos guardados
                                    if (Activos[a].nombreCuenta.Equals(nombreCuenta))
                                    {
                                        Activos[a].debe = Nodo.Blocks[i].Cuentas[j].debe + Activos[a].debe;
                                        Activos[a].haber = Nodo.Blocks[i].Cuentas[j].haber + Activos[a].haber;

                                        Activos[a].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                                        break;
                                    }
                                    else if (a == Activos.Count - 1)
                                    {
                                        Activo nuevoActivo = new Activo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        Activos.Add(nuevoActivo);
                                        Activos[a+1].registros = new List<Cuenta>();
                                        Activos[a+1].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                                    }
                                }
                            }
                            else
                            {
                                
                                Activo nuevoActivo = new Activo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                Activos = new List<Activo>();
                                Activos.Add(nuevoActivo);

                                //Inicializacion de lista de resgistros en el nuevo activo
                                Activos[0].registros = new List<Cuenta>();
                                Activos[0].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                            }


                            break;
                        //Pasivo
                        case 2:
                            //Si la lista de pasivos no es nula
                            if (Pasivos != null)
                            {


                                //Pasar por cada pasivo en la lista
                                for (int a = 0; a < Pasivos.Count; a++)
                                {
                                    //Comparar nombre de la Cuenta con los nombres de los pasivos guardados
                                    if (Pasivos[a].nombreCuenta.Equals(nombreCuenta))
                                    {
                                        Pasivos[a].debe = Nodo.Blocks[i].Cuentas[j].debe + Activos[a].debe;
                                        Pasivos[a].haber = Nodo.Blocks[i].Cuentas[j].haber + Activos[a].haber;

                                        Pasivos[a].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                                        break;
                                    }
                                    else if(a == Pasivos.Count - 1)
                                    {
                                        Pasivo nuevoPasivo = new Pasivo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        Pasivos.Add(nuevoPasivo);

                                        Pasivos[a + 1].registros = new List<Cuenta>();
                                        Pasivos[a + 1].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                                    }
                                }
                            }
                            else
                            {
                                Pasivo nuevoPasivo = new Pasivo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                Pasivos = new List<Pasivo>();
                                Pasivos.Add(nuevoPasivo);

                                //Inicializacion de lista de resgistros en el nuevo activo
                                Pasivos[0].registros = new List<Cuenta>();
                                Pasivos[0].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                            }

                            break;

                        //Patrimonio Neto
                        case 3:
                            if (PatrimoniosNetos != null)
                            {

                                for (int a = 0; a < PatrimoniosNetos.Count; a++)
                                {

                                    if (PatrimoniosNetos[a].nombreCuenta.Equals(nombreCuenta))
                                    {
                                        PatrimoniosNetos[a].saldoAcreedor = Nodo.Blocks[i].Cuentas[j].haber + PatrimoniosNetos[a].saldoAcreedor;

                                        PatrimoniosNetos[a].registros.Add(Nodo.Blocks[i].Cuentas[j]);

                                        break;
                                    }
                                    else if (a == PatrimoniosNetos.Count - 1)
                                    {
                                        PatrimonioNet nuevoPatrimonioN = new PatrimonioNet(Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        PatrimoniosNetos.Add(nuevoPatrimonioN);

                                        PatrimoniosNetos[a + 1].registros = new List<Cuenta>();
                                        PatrimoniosNetos[a + 1].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                                    }
                                }
                            }
                            else
                            {
                                PatrimonioNet nuevoPatrimonioN = new PatrimonioNet(Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                PatrimoniosNetos = new List<PatrimonioNet>();
                                PatrimoniosNetos.Add(nuevoPatrimonioN);

                                //Inicializacion de lista de resgistros en el nuevo activo
                                PatrimoniosNetos[0].registros = new List<Cuenta>();
                                PatrimoniosNetos[0].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                            }

                            break;
                        //Resultado Negativo
                        case 4:
                            if (ResultadosNegativos != null)
                            {



                                for (int a = 0; a < ResultadosNegativos.Count; a++)
                                {

                                    if (ResultadosNegativos[a].nombreCuenta.Equals(nombreCuenta))
                                    {
                                        ResultadosNegativos[a].saldoDeudor = Nodo.Blocks[i].Cuentas[j].haber + ResultadosNegativos[a].saldoDeudor;

                                        ResultadosNegativos[a].registros.Add(Nodo.Blocks[i].Cuentas[j]);

                                        break;
                                    }
                                    else if (a == ResultadosNegativos.Count - 1)
                                    {
                                        ResultadoNegativo nuevoNegativo = new ResultadoNegativo(Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        ResultadosNegativos.Add(nuevoNegativo);

                                        ResultadosNegativos[a + 1].registros = new List<Cuenta>();
                                        ResultadosNegativos[a + 1].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                                    }
                                }
                            }
                            else
                            {
                                ResultadoNegativo nuevoNegativo = new ResultadoNegativo(Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                ResultadosNegativos = new List<ResultadoNegativo>();
                                ResultadosNegativos.Add(nuevoNegativo);

                                //Inicializacion de lista de resgistros en el nuevo activo
                                ResultadosNegativos[0].registros = new List<Cuenta>();
                                ResultadosNegativos[0].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                            }
                            break;
                        //Resultado Positivo
                        default:
                            if (ResultadosPositivos != null)
                            {

                                for (int a = 0; a < ResultadosPositivos.Count; a++)
                                {
 
                                    if (ResultadosPositivos[a].nombreCuenta.Equals(nombreCuenta))
                                    {
                                        ResultadosPositivos[a].saldoAcreedor = Nodo.Blocks[i].Cuentas[j].debe + ResultadosPositivos[a].saldoAcreedor;

                                        ResultadosPositivos[a].registros.Add(Nodo.Blocks[i].Cuentas[j]);

                                        break;
                                    }
                                    else if (a == ResultadosPositivos.Count - 1)
                                    {
                                        ResultadoPositivo nuevoPositivo = new ResultadoPositivo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        ResultadosPositivos.Add(nuevoPositivo);

                                        ResultadosPositivos[a + 1].registros = new List<Cuenta>();
                                        ResultadosPositivos[a + 1].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                                    }
                                }
                            }
                            else
                            {
                                ResultadoPositivo nuevoPositivo = new ResultadoPositivo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                ResultadosPositivos = new List<ResultadoPositivo>();
                                ResultadosPositivos.Add(nuevoPositivo);

                                //Inicializacion de lista de resgistros en el nuevo activo
                                ResultadosPositivos[0].registros = new List<Cuenta>();
                                ResultadosPositivos[0].registros.Add(Nodo.Blocks[i].Cuentas[j]);
                            }
                            break;
                    }


                }
            }
            if(Activos != null)
            {
                //Calcular saldos deudores de los activos
                for (int a = 0; a < Activos.Count; a++)
                {
                    Activos[a].saldoDeudor = Activos[a].debe - Activos[a].haber;
                }

            }

            if(Pasivos != null)
            {
                //Calcular saldos acreedores de los pasivos
                for (int a = 0; a < Pasivos.Count; a++)
                {
                    Pasivos[a].saldoAcreedor = Pasivos[a].haber - Pasivos[a].debe;
                }
            }
           
        }


        public void mostrarMayores()
        {
            //Cuenta los totales de cada fila
            double totalDebe = 0;
            double totalHaber = 0;
            double totalSD = 0;
            double totalSA = 0;

            // -------------------- IMPRESION DEL LIBRO MAYOR ---------------------------
            Console.WriteLine("\n\n\n");
            Console.WriteLine("----  LIBRO DE MAYORES  ----");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            if(Activos != null)
            {
                for (int a = 0; a < Activos.Count; a++)
                {
                    totalDebe += Activos[a].debe;
                    totalHaber += Activos[a].haber;
                    totalSD += Activos[a].saldoDeudor;

                    Console.Write("| " + Activos[a].nombreCuenta + "          | ");
                    Console.Write(Activos[a].debe + "   | ");
                    Console.Write(Activos[a].haber + "   | ");
                    Console.Write(Activos[a].saldoDeudor + "   | ");
                    Console.WriteLine("0        | ");
                }
            }
            if (Pasivos != null)
            {
                for (int a = 0; a < Pasivos.Count; a++)
                {
                    totalDebe += Pasivos[a].debe;
                    totalHaber += Pasivos[a].haber;
                    totalSA += Pasivos[a].saldoAcreedor;


                    Console.Write("| " + Pasivos[a].nombreCuenta + "          | ");
                    Console.Write(Pasivos[a].debe + "   | ");
                    Console.Write(Pasivos[a].haber + "   | ");
                    Console.Write("0        | ");
                    Console.WriteLine(Pasivos[a].saldoAcreedor + "   | ");
                }
            }
            if(PatrimoniosNetos != null)
            {
                

                for (int a = 0; a < PatrimoniosNetos.Count; a++)
                {
                    totalHaber += PatrimoniosNetos[a].saldoAcreedor;
                    totalSA += PatrimoniosNetos[a].saldoAcreedor;



                    Console.Write("| " + PatrimoniosNetos[a].nombreCuenta + "          | ");
                    Console.Write("0        | ");
                    Console.Write(PatrimoniosNetos[a].saldoAcreedor + "   | ");
                    Console.Write("0        | ");
                    Console.WriteLine(PatrimoniosNetos[a].saldoAcreedor + "   | ");
                }
            }

            if (ResultadosPositivos != null)
            {
                for (int a = 0; a < ResultadosPositivos.Count; a++)
                {

                    totalHaber += ResultadosPositivos[a].saldoAcreedor;
                    totalSA += ResultadosPositivos[a].saldoAcreedor;


                    Console.Write("| " + ResultadosPositivos[a].nombreCuenta + "          | ");
                    Console.Write("0        | ");
                    Console.Write(ResultadosPositivos[a].saldoAcreedor + "   | ");
                    Console.Write("0        | ");
                    Console.WriteLine(ResultadosPositivos[a].saldoAcreedor + "   | ");
                }
            }
            if (ResultadosNegativos != null)
            {
                for (int a = 0; a < ResultadosNegativos.Count; a++)
                {
                    totalDebe += ResultadosNegativos[a].saldoDeudor;
                    totalSD += ResultadosNegativos[a].saldoDeudor;




                    Console.Write("| " + ResultadosNegativos[a].nombreCuenta + "          | ");
                    Console.Write(ResultadosNegativos[a].saldoDeudor + "   | ");
                    Console.Write("0        | ");
                    Console.Write(ResultadosNegativos[a].saldoDeudor + "   | ");
                    Console.WriteLine("0        | ");
                }
            }

            // ----------------------- IMPRESION DE LOS TOTALES ---------------------------
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("TOTAL DEBE= "+ totalDebe);
            Console.WriteLine("TOTAL HABER= " + totalHaber);
            Console.WriteLine("SALDO DEUDOR TOTAL= " + totalSD);
            Console.WriteLine("SALDO ACREEDOR TOTAL= " + totalSA);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
        }


        public void mostrarMayorElegido()
        {
            //Se guarda el formato en el que debe estar la fecha
            var cultureInfo = new CultureInfo("nl-NL");
            
            int tipo = 0;
            int numCuenta = 0;
            string fecha;

            DateTime fechaInicio;
            DateTime fechaFin;

            do
            {
                Console.WriteLine("Ingrese la fecha de inicio del intervalo necesitado: xx/xx/xxxx");

                //Lee y valida que la fecha sea correcta
                do
                {
                    fecha = Console.ReadLine();
                } while (!DateTime.TryParse(fecha, out DateTime fechaA));

                //Guarda la fecha de inicio en formato DateTime
                fechaInicio = DateTime.Parse(fecha, cultureInfo, DateTimeStyles.NoCurrentDateDefault);

                Console.WriteLine("Ingrese la fecha de fin del intervalo necesitado: xx/xx/xxxx");

                //Lee y valida que la fecha sea correcta
                do
                {
                    fecha = Console.ReadLine();
                } while (!DateTime.TryParse(fecha, out DateTime fechaB));

                //Guarda la fecha de inicio en formato DateTime
                fechaFin = DateTime.Parse(fecha, cultureInfo, DateTimeStyles.NoCurrentDateDefault);
            } while (fechaFin < fechaInicio); //Si fin esta antes que principio
            


            Console.WriteLine("Elija el tipo de cuenta del que quiere mostrar su mayor: ");
            Console.WriteLine("1_Activo   2_Pasivo   3_Patrimonio Neto   4_Resultado Negativo   5_Resultado Positivo");

            try
            {
                do
                {
                    tipo = Convert.ToInt32(Console.ReadLine());

                } while (tipo > 5 || tipo < 1 || tipo == null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en el ingreso, intente de nuevo.");
                mostrarMayorElegido();
            }

            Console.WriteLine("Ingrese el numero de la cuenta: ");
            switch (tipo)
            {
                //Activo
                case 1:

                    if(Activos != null)
                    {
                        //Muestra la lista de cuentas
                        for (int i = 0; i < Activos.Count; i++)
                        {
                            Console.Write((i + 1) + "- ");
                            Console.WriteLine(Activos[i].nombreCuenta);
                        }

                        //Lee el numero de la cuenta que el usuario quiere ver
                        try
                        {
                            do
                            {
                                numCuenta = Convert.ToInt32(Console.ReadLine());

                            } while (numCuenta > Activos.Count + 1 || numCuenta < 1 || numCuenta == null);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error en el ingreso, intente de nuevo.");
                            mostrarMayorElegido();
                        }

                        if(Activos[numCuenta - 1].registros != null)
                        {
                            Console.WriteLine(Activos[numCuenta - 1].registros[0].fecha);

                            //Llamar a metodo que imprime los registros
                            imprimirRegistros(Activos[numCuenta - 1].registros, fechaInicio, fechaFin);
                        }
                        

                    }

                    break;
                //Pasivo
                case 2:
                    if (Pasivos != null)
                    {
                        for (int i = 0; i < Pasivos.Count; i++)
                        {
                            Console.Write((i + 1) + "- ");
                            Console.WriteLine(Pasivos[i].nombreCuenta);
                        }

                        //Lee el numero de la cuenta que el usuario quiere ver
                        try
                        {
                            do
                            {
                                numCuenta = Convert.ToInt32(Console.ReadLine());

                            } while (numCuenta > Pasivos.Count + 1 || numCuenta < 1 || numCuenta == null);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error en el ingreso, intente de nuevo.");
                            mostrarMayorElegido();
                        }

                        if (Pasivos[numCuenta - 1].registros != null)
                        {
                            Console.WriteLine(Pasivos[numCuenta - 1].registros[0].fecha);

                            //Llamar a metodo que imprime los registros
                            imprimirRegistros(Pasivos[numCuenta - 1].registros, fechaInicio, fechaFin);
                        }

                    }


                    break;
                //Patrimonio Neto
                case 3:
                    if (PatrimoniosNetos != null)
                    {
                        for (int i = 0; i < PatrimoniosNetos.Count; i++)
                        {
                            Console.Write((i + 1) + "- ");
                            Console.WriteLine(PatrimoniosNetos[i].nombreCuenta);
                        }
                        //Lee el numero de la cuenta que el usuario quiere ver
                        try
                        {
                            do
                            {
                                numCuenta = Convert.ToInt32(Console.ReadLine());

                            } while (numCuenta > PatrimoniosNetos.Count + 1 || numCuenta < 1 || numCuenta == null);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error en el ingreso, intente de nuevo.");
                            mostrarMayorElegido();
                        }

                        if (PatrimoniosNetos[numCuenta - 1].registros != null)
                        {
                            Console.WriteLine(PatrimoniosNetos[numCuenta - 1].registros[0].fecha);

                            //Llamar a metodo que imprime los registros
                            imprimirRegistros(PatrimoniosNetos[numCuenta - 1].registros, fechaInicio, fechaFin);
                        }
                    }


                    break;
                //Resultado Negativo
                case 4:
                    if (ResultadosNegativos != null)
                    {
                        for (int i = 0; i < ResultadosNegativos.Count; i++)
                        {
                            Console.Write((i + 1) + "- ");
                            Console.WriteLine(ResultadosNegativos[i].nombreCuenta);
                        }
                        //Lee el numero de la cuenta que el usuario quiere ver
                        try
                        {
                            do
                            {
                                numCuenta = Convert.ToInt32(Console.ReadLine());

                            } while (numCuenta > ResultadosNegativos.Count + 1 || numCuenta < 1 || numCuenta == null);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error en el ingreso, intente de nuevo.");
                            mostrarMayorElegido();
                        }

                        if (ResultadosNegativos[numCuenta - 1].registros != null)
                        {
                            Console.WriteLine(ResultadosNegativos[numCuenta - 1].registros[0].fecha);

                            //Llamar a metodo que imprime los registros
                            imprimirRegistros(ResultadosNegativos[numCuenta - 1].registros, fechaInicio, fechaFin);
                        }
                    }


                    break;
                //Resultado Positivo
                case 5:
                    if (ResultadosPositivos != null)
                    {
                        for (int i = 0; i < ResultadosPositivos.Count; i++)
                        {
                            Console.Write((i + 1) + "- ");
                            Console.WriteLine(ResultadosPositivos[i].nombreCuenta);
                        }
                        //Lee el numero de la cuenta que el usuario quiere ver
                        try
                        {
                            do
                            {
                                numCuenta = Convert.ToInt32(Console.ReadLine());

                            } while (numCuenta > ResultadosPositivos.Count + 1 || numCuenta < 1 || numCuenta == null);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error en el ingreso, intente de nuevo.");
                            mostrarMayorElegido();
                        }

                        if (ResultadosPositivos[numCuenta - 1].registros != null)
                        {
                            Console.WriteLine(ResultadosPositivos[numCuenta - 1].registros[0].fecha);

                            //Llamar a metodo que imprime los registros
                            imprimirRegistros(ResultadosPositivos[numCuenta - 1].registros, fechaInicio, fechaFin);
                        }
                    }


                    break;
            }


        }

        public void imprimirRegistros(List<Cuenta> registros, DateTime fechaInicio, DateTime fechaFin)
        {

            //Se guarda el formato en el que debe estar la fecha
            var cultureInfo = new CultureInfo("nl-NL");
            DateTime fechaReg;
            double debet = 0;
            double habert = 0;
            int tipo = registros[0].tipo;

            Console.WriteLine("------------ " + registros[0].nombre.ToUpper() + " ------------- ");
            

            //Pasa por todos los registros de esa cuenta
            for (int i = 0; i < registros.Count; i++)
            {
                
                //Guarda la fecha del registro de inicio en formato DateTime
                fechaReg = DateTime.Parse(registros[i].fecha, cultureInfo, DateTimeStyles.NoCurrentDateDefault);

                //Si la fecha del registro esta en el intervalo pedido
                if(fechaReg >= fechaInicio && fechaReg <= fechaFin)
                {
                    Console.Write("| "+registros[i].fecha+" | ");
                    Console.Write(registros[i].debe + "  | ");
                    Console.WriteLine(registros[i].haber);

                    debet += registros[i].debe;
                    habert += registros[i].haber;
                }

                
            }
            Console.WriteLine("--------------------------------------------\n");
            Console.WriteLine("TOTAL DEBE: " + debet);
            Console.WriteLine("TOTAL HABER: " + habert);

            if (tipo == 1 || tipo == 4)
            {
                Console.WriteLine("SALDO DEUDOR: " + (debet - habert));
            }
            else
            {
                Console.WriteLine("SALDO ACREEDOR: " + (habert - debet));
            }
        }


    }
}
