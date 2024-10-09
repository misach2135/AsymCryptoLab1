using System;

namespace LehmerGenerator  
{
    public class LehmerGenerator
    {
        private const ulong m = 4294967296;
        private const ulong a = 65537;
        private const ulong c = 119;

        private ulong _seed;

        public LehmerGenerator(ulong seed)
        {
            _seed = seed;
        }
        public byte LehmerLow()
        {
            _seed = (a * _seed + c) % m;
            return (byte)(_seed & 0xFF);
        }

        public byte LehmerHigh()
        {
            _seed = (a * _seed + c) % m;
            return (byte)((_seed >> 24) & 0xFF);
        }
    }
}