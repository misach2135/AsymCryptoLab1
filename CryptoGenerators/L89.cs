using BitWizzardy;
using CryptoGenerators;
using System;
using System.Collections;

namespace CryptoGenerators
{
    public class L89Generator : IGenerator
    {
        private LFSR128 lfsr;

        public L89Generator()
        {
            lfsr = new(0, 89, [0, 51]);
        }

        public BitArray GenBits(long seed, int length)
        {
            BitArray res = new BitArray(length);
            lfsr.State = new UInt128(0, (ulong)seed);
            for (int i = 0; i < length; i++)
            {
                res[i] = lfsr.Next() == 1;
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