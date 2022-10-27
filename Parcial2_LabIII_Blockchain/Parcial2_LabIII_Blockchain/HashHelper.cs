using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_LabIII_Blockchain
{
    internal class HashHelper
    {

        public static string CalculateHash(string text)
        {

            string myHashCalculated = string.Empty;

            //calcular el hash
            using (SHA256 mySHA256 = SHA256.Create())
            {

                //selecionar el encoding "utf8". Pasamos el texto a un array de bytes con el encoding de UTF8
                byte[] encodedText = new UTF8Encoding().GetBytes(text);

                //Calculamos el Hash. Tenemos en un array de bytes el texto codificado con SHA256
                byte[] myHashArray = mySHA256.ComputeHash(encodedText);

                //Convertimos en un string el hash codificado
                myHashCalculated = BitConverter.ToString(myHashArray).Replace("-", string.Empty);          //Replace("-", string.Empty) ==> el byte array tiene guines en medio(segun el que explica en el video), esta funcion quita los guiones
                //myHashCalculated = BitConverter.ToString(myHashArray);
            }


            return myHashCalculated;
        }

    }
}
