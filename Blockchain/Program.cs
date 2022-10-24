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


        public interface IBlock
        {
            byte[] Data { get;}
            byte[] Hash { get; set; }
            int Nonce { get; set; }

            byte[] PrevHash { get; set; }

            DateTime TimeStamp { get; }
        }

        public class Block:IBlock
        {
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

            public override string ToString()
            {
                return $"{BitConverter.ToString(Hash).Replace("-", "")}:\n{BitConverter.ToString(PrevHash).Replace("-", "")}\n {Nonce}  {TimeStamp}";
            }

        }

        public class Blockchain : IEnumerable<IBlock>
        {
            private List<IBlock> items = new List<IBlock>();

            public Blockchain(byte[] difficulty, IBlock genesis){



            }

            public int Count => items.Count;

            public IBlock this[int index] => items[index];

            public List<IBlock> Items => items;


            public IEnumerator GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator<IBlock> IEnumerable<IBlock>.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

    }
}
