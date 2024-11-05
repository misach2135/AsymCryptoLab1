using System;
using System.Collections;


namespace CryptoGenerators
{
    public class NativeGenerator : IGenerator
    {
        public BitArray GenBits(long seed, int length)
        {
            var remainder = length % 8;
            var bytes = GenBytes(seed, (length / 8) + Convert.ToByte(remainder != 0));
            BitArray bitArray = new BitArray(bytes);
            if (remainder != 0)
            {
                bitArray.Length -= (8 - remainder);
            }
            return bitArray;
        }

        public byte[] GenBytes(long seed, int length)
        {
            var rand = new Random();
            byte[] bytes = new byte[length];
            rand.NextBytes(bytes);
            return bytes;
        }
    }
}
