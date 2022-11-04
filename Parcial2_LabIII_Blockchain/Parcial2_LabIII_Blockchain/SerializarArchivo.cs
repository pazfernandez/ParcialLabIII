using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class SerializarArchivo
    {

        private FileStream miArchivo;
        private BinaryFormatter Convertidor;

        public bool GuardarCadena(Blockchain blockchain)
        {
            try
            {
                miArchivo = new FileStream("archivo.dat", FileMode.OpenOrCreate, FileAccess.Write);
                Convertidor = new BinaryFormatter();
                Convertidor.Serialize(miArchivo, blockchain);
                miArchivo.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Blockchain LeerCadena()
        {
            Blockchain cadena = null;

            try
            {
                miArchivo = new FileStream("archivo.dat", FileMode.Open, FileAccess.Read);
                Convertidor = new BinaryFormatter();
                cadena = (Blockchain)Convertidor.Deserialize(miArchivo);
                miArchivo.Close();
                return cadena;
            }
            catch (Exception ex)
            {
                Blockchain newBlockchain = new Blockchain();
                return newBlockchain;

                /*Console.WriteLine(ex.Message);
                return null;*/
            }
        }

        //Si trabajamos con listas

        /*public bool GuardarListaCadena(List<Blockchain> ListaBlockchain)
        {
            try
            {
                miArchivo = new FileStream("archivo.dat", FileMode.OpenOrCreate, FileAccess.Write);
                Convertidor = new BinaryFormatter();
                Convertidor.Serialize(miArchivo, ListaBlockchain);
                miArchivo.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Blockchain> LeerListaCadena()
        {
            List<Blockchain> listaCadena = null;

            try
            {
                miArchivo = new FileStream("archivo.dat", FileMode.Open, FileAccess.Read);
                Convertidor = new BinaryFormatter();
                listaCadena = (List<Blockchain>)Convertidor.Deserialize(miArchivo);
                miArchivo.Close();
                return listaCadena;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }*/

    }
}
