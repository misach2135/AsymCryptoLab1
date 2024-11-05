using BitWizzardy;
using CryptoGenerators;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CryptoGenerators
{

    public class L20Generator : IGenerator
    {
        private LFSR128 lfsr;

        public L20Generator()
        {
            lfsr = new(0, 20, [0, 11, 15, 17]);
            //lfsr = new(0, 20, [2, 4, 8, 19]);
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
