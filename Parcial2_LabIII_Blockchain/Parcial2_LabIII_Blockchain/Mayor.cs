using System;
using System.Collections.Generic;
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
                                        break;
                                    }
                                    else if (a == Activos.Count - 1)
                                    {
                                        Activo nuevoActivo = new Activo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        Activos.Add(nuevoActivo);
                                    }
                                }
                            }
                            else
                            {
                                Activo nuevoActivo = new Activo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                Activos = new List<Activo>();
                                Activos.Add(nuevoActivo);
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
                                        break;
                                    }
                                    else if(a == Pasivos.Count - 1)
                                    {
                                        Pasivo nuevoPasivo = new Pasivo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        Pasivos.Add(nuevoPasivo);
                                    }
                                }
                            }
                            else
                            {
                                Pasivo nuevoPasivo = new Pasivo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                Pasivos = new List<Pasivo>();
                                Pasivos.Add(nuevoPasivo);
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

                                        break;
                                    }
                                    else if (a == PatrimoniosNetos.Count - 1)
                                    {
                                        PatrimonioNet nuevoPatrimonioN = new PatrimonioNet(Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        PatrimoniosNetos.Add(nuevoPatrimonioN);
                                    }
                                }
                            }
                            else
                            {
                                PatrimonioNet nuevoPatrimonioN = new PatrimonioNet(Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                PatrimoniosNetos = new List<PatrimonioNet>();
                                PatrimoniosNetos.Add(nuevoPatrimonioN);
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

                                        break;
                                    }
                                    else if (a == ResultadosNegativos.Count - 1)
                                    {
                                        ResultadoNegativo nuevoNegativo = new ResultadoNegativo(Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        ResultadosNegativos.Add(nuevoNegativo);
                                    }
                                }
                            }
                            else
                            {
                                ResultadoNegativo nuevoNegativo = new ResultadoNegativo(Nodo.Blocks[i].Cuentas[j].haber, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                ResultadosNegativos = new List<ResultadoNegativo>();
                                ResultadosNegativos.Add(nuevoNegativo);
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

                                        break;
                                    }
                                    else if (a == ResultadosPositivos.Count - 1)
                                    {
                                        ResultadoPositivo nuevoPositivo = new ResultadoPositivo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                        ResultadosPositivos.Add(nuevoPositivo);
                                    }
                                }
                            }
                            else
                            {
                                ResultadoPositivo nuevoPositivo = new ResultadoPositivo(Nodo.Blocks[i].Cuentas[j].debe, Nodo.Blocks[i].Cuentas[j].nombre.ToLower().Trim());
                                ResultadosPositivos = new List<ResultadoPositivo>();
                                ResultadosPositivos.Add(nuevoPositivo);
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
    }
}
