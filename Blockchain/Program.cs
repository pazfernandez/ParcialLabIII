using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

        //Interfaz del bloque, establece su composicion
        public interface IBlock
        {
            //Datos que se guaradaran
            byte[] Data { get;}
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
        public class Block:IBlock
        {
            //Todos los getter y setters de los atributos del bloque
            byte[] Data { get; }
            byte[] IBlock.Data => throw new NotImplementedException();

            byte[] Hash { get; set; }
            byte[] IBlock.Hash { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            int Nonce { get; set; }
            int IBlock.Nonce { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            byte[] PrevHash { get; set; }
            byte[] IBlock.PrevHash { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            DateTime TimeStamp { get; }
            DateTime IBlock.TimeStamp => throw new NotImplementedException();


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
            private List<IBlock> Items = new List<IBlock>();

            //Crea el primer bloque al que le llama "genesis"
            public Blockchain(byte[] difficulty, IBlock genesis){

                Difficulty = difficulty;
                genesis.Hash = genesis.MineHash(difficulty);
                Items.Add(genesis);

            }

            //Cuando se añade un bloque a la lista
            public void Add(IBlock item)
            {
                //Si el bloque ultimo de la lista, antes de agregar este no es nulo
                if(Items.LastOrDefault() != null)
                {
                    //El prevHash de este es el Hash del anterior
                    item.PrevHash = Items.LastOrDefault()?.Hash
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

            public byte [] Difficulty { get;}

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

    }
}
