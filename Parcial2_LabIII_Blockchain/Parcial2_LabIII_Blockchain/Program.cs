using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }
}
