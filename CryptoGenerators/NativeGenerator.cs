using System;
using System.Collections;


namespace CryptoGenerators
{
    public class NativeGenerator : IGenerator
    {
        public BitArray GenBits(long seed, int length)
        {
            var rand = new Random();
            byte[] bytes = new byte[length];
            BitArray bitok = new BitArray(length); //no binance
            rand.NextBytes(bytes);
            bitok.CopyTo(bytes, 0);
            return bitok;
        }
    }
}
