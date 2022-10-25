using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Blockchain.Program;

namespace Blockchain
{


    //Funciones de generacion y validacion hash
    public static class BlockchainExtension
    {
        //Genera el Hash a partir de todos los datos que integran el bloque con un objeto de la clase SHA512
        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream st = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(st))
            {
                bw.Write(block.Data);
                bw.Write(block.Nonce);
                bw.Write(block.TimeStamp.ToBinary());
                bw.Write(block.PrevHash);

                var starr = st.ToArray();

                //Devuelve una serie de caracteres random que solo pueden ser generados con exactamente los mismos datos
                return sha.ComputeHash(starr);
            }

        }

        public static byte[] MineHash(this IBlock block, byte[] difficulty)
        {

            if (difficulty == null) throw new ArgumentNullException(nameof(difficulty));

            //Genera el hash del bloque segun la dificultad pasada como parametro
            byte[] hash = new byte[0];
            int d = difficulty.Length;

            while (!hash.Take(2).SequenceEqual(difficulty))
            {
                block.Nonce++;
                hash = block.GenerateHash();
            }

            return hash;
        }

        public static bool IsValid(this IBlock block)
        {

            //Genera el hash del bloque
            var bk = block.GenerateHash();

            //Compara el hash ya guardado en el bloque que le pasamos con el que generamos
            return block.Hash.SequenceEqual(bk);
        }

        public static bool IsValidPrevBlock(this IBlock block, IBlock prevBlock)
        {

            //Si el bloque previo es null tira una excepcion
            if (prevBlock == null) throw new ArgumentNullException(nameof(prevBlock));

            //Generamos el hash del bloque anterior
            var prev = prevBlock.GenerateHash(); //var prev = prevBlock.Hash.GenerateHash();

            //Ve si el bloque previo es valido en cuanto al hash y compara el hash del bloque anterior con el hash del bloque anterior guardado en el siguiente
            return prevBlock.IsValid() && block.PrevHash.SequenceEqual(prev);

        }


        //Validamos un bloque
        public static bool IsValid(this IEnumerable<IBlock> items)
        {
            var enumerable = items.ToList();
            return enumerable.Zip(enumerable.Skip(1), Tuple.Create).All(block => block.Item2.IsValid() && block.Item2.IsValidPrevBlock(block.Item1)); //EL HASH ES HASH O ???

            //return enumerable.Zip(enumerable.Skip(1), Tuple.Create).All(block => block.Item2.IsValid() && block.Item2.Hash);
        }
    }



    //Interfaz del bloque, establece su composicion
     public interface IBlock
    {
        //Datos que se guaradaran
        byte[] Data { get; }
        //El hash creado con los datos que guarda ESTE bloque
        byte[] Hash { get; set; }
        //???
        int Nonce { get; set; }
        //Hash del anterior bloque a este
        byte[] PrevHash { get; set; }
        //Tiempo de la creacion del bloque
        DateTime TimeStamp { get; }
    }

    //Clase bloque que implementa la interfaz
    public class Block : IBlock
    {
        //Constructor Basico
        public Block(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Nonce = 0;
            PrevHash = new byte[] { 0x00 };
            TimeStamp = DateTime.Now;

        }


        //Todos los getter y setters de los atributos del bloque
        public byte[] Data { get; }
        //byte[] IBlock.Data => throw new NotImplementedException();

        public byte[] Hash { get; set; }
       //byte[] IBlock.Hash { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Nonce { get; set; }
        //int IBlock.Nonce { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public byte[] PrevHash { get; set; }
        //byte[] IBlock.PrevHash { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime TimeStamp { get; }
        //DateTime IBlock.TimeStamp => throw new NotImplementedException();


        //Hace un override del tostring original para mostrar la composicion del bloque cuando se lo muestra
        public override string ToString()
        {
            return $"{BitConverter.ToString(Hash).Replace("-", "")}:\n{BitConverter.ToString(PrevHash).Replace("-", "")}\n {Nonce}  {TimeStamp}";
        }

    }

    //Clase para la creacion de una lista de bloques
    public class Blockchain : IEnumerable<IBlock>
    {
        //Crea la lista
        private List<IBlock> items = new List<IBlock>();

        //Crea el primer bloque al que le llama "genesis"
        public Blockchain(byte[] difficulty, IBlock genesis)
        {

            Difficulty = difficulty;
            genesis.Hash = genesis.MineHash(difficulty);
            Items.Add(genesis);

        }

        //Cuando se añade un bloque a la lista
        public void Add(IBlock item)
        {
            //Si el bloque ultimo de la lista, antes de agregar este no es nulo
            if (Items.LastOrDefault() != null)
            {
                //El prevHash de este es el Hash del anterior
                item.PrevHash = Items.LastOrDefault()?.Hash;
            }
            //El hash de este va a ser creado con una funcion hash a nuestra eleccion
            item.Hash = item.MineHash(Difficulty);
            Items.Add(item);
        }


        //Contador de bloques en la lista
        public int Count => Items.Count;

        //public IBlock this[int index] => items[index];

        public IBlock this[int index]
        {
            //Getter segun el index pasado por parametro
            get => Items[index];
            //Set del valor del item segun el index pasado por parametro
            set => Items[index] = value;
        }


        public List<IBlock> Items => items;

        public byte[] Difficulty { get; }

        public IEnumerator<IBlock> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }


        /*IEnumerator<IBlock> IEnumerable<IBlock>.GetEnumerator()
        {
            throw new NotImplementedException();
        }*/
    }

   
    internal class Program
    {
        static void Main(string[] args)
        {

            Random rnd = new Random(DateTime.UtcNow.Millisecond);

            //Generar el primer bloque de la cadena
            IBlock genesis = new Block(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 });

            byte[] difficulty = new byte[] { 0x00, 0x00 };

            //Genera una nueva cadena ya con el primer bloque 
            Blockchain chain = new Blockchain(difficulty, genesis);

            //Generara 200 bloques
            for (int i = 0; i < 3; i++)
            {
                var data = Enumerable.Range(0, 2256).Select(p => (byte)rnd.Next());
                chain.Add(new Block(data.ToArray()));
                Console.WriteLine(chain.LastOrDefault()?.ToString());

                Console.WriteLine("Cadena esta validada: " + chain.IsValid());
            }

            Console.WriteLine("Primer bloque: " + chain[0].ToString());

            Console.ReadLine();




        }



    }
}
