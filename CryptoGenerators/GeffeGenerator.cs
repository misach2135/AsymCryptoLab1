using BitWizzardy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGenerators
{
    public class GeffeGenerator : IGenerator
    {
        private LFSR128 L11;
        private LFSR128 L9;
        private LFSR128 L10;

        public GeffeGenerator()
        {
            L11 = new(0, 11, [0, 2]);
            L9 = new(0, 9, [0, 1, 3, 4]);
            L10 = new(0, 10, [0, 3]);
        }

        public BitArray GenBits(long seed, int length)
        {

            L11.State = new UInt128(0, (ulong)seed) & 0x7FF;
            L9.State = (new UInt128(0, (ulong)seed) & 0x1FF0);
            L10.State = (new UInt128(0, (ulong)seed) & 0xFFC); 

            Func<int> nextBit = () =>
            {
                int s = L10.Next();
                if (s == 1)
                {
                    return L11.Next();
                }
                else
                {
                    return L9.Next();
                }
            };

            BitArray res = new(length);
            for (int i = 0; i < length; i++)
            {
                res[i] = nextBit() == 1;
            }

            return res;
        }

        public byte[] GenBytes(long seed, int length)
        {
            byte[] res = new byte[length];
            var bits = GenBits(seed, length * 8);
            bits.CopyTo(res, 0);
            return res;
        }
    }
}
