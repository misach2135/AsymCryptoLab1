using System;
using System.Collections;

namespace CryptoGenerators
{

    public class LehmerGenerator : IGenerator
    {
        public enum LehmerMode
        {
            Low, High
        }

        private const long m = 1L << 32;
        private const long a = (1 << 16) + 1;
        private const long c = 119;

        private LehmerMode _mode;

        public LehmerMode Mode { get => _mode; }

        public LehmerGenerator(LehmerMode mode)
        {
            _mode = mode;
        }

        public BitArray GenBits(long seed, int length)
        {
            byte[] bytes = GenBytes(seed, length / 8);
            BitArray bitArray = new BitArray(bytes);
            return bitArray;
        }

        public byte[] GenBytes(long seed, int length)
        {
            byte[] res = new byte[length];
            long x = seed % m;
            for (int i = 0; i < length; i++)
            {
                x = (a * x + c) % m;
                byte _out = 0;
                switch (_mode)
                {
                    case LehmerMode.Low:
                        _out = Convert.ToByte(x & 0b1111_1111);
                        break;
                    case LehmerMode.High:
                        _out = Convert.ToByte((x & (0b1111_1111L << 8)) >> 8);
                        break;
                }
                res[i] = _out;
            }

            return res;
        }
    }
}