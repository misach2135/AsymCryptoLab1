using CryptoGenerators;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CryptoGenerators
{

    public class L20Generator : IGenerator
    {
        private BitArray register;
        private int period;

        public L20Generator()
        {
            register = new BitArray(20);

            period = (1 << 20) - 1;
        }

        public void Initialize(string initialValues)
        {
            if (initialValues.Length < 20)
            {
                initialValues = initialValues.PadLeft(20, '0');
            }

            if (initialValues.Length > 20)
            {
                initialValues = initialValues.Remove(20, initialValues.Length - 20);
            }

            for (int i = 0; i < 20; i++)
            {
                register[i] = initialValues[i] == '1';
            }
        }
        public bool NextBit()
        {
            bool nextBit = register[2] ^ register[4] ^ register[8] ^ register[19];

            for (int i = 19; i > 0; i--)
            {
                register[i] = register[i - 1];
            }

            register[0] = nextBit;

            return nextBit;
        }

        public BitArray GenBits(long seed, int length)
        {
            Initialize(Convert.ToString(seed, 2));
            BitArray sequence = new(length);

            for (int i = 0; i < length; i++)
            {
                sequence[i] = NextBit();
            }

            return sequence;
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
