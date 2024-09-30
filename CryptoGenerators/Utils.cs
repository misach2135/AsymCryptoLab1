using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGenerators
{
    public static class Utils
    {
        public static uint RightCyclicShift(uint num, int t = 1)
        {
            return (num >> t) | (num << (32 - t));
        }

        public static uint LeftCyclicShift(uint num, int t = 1)
        {
            return (num << t) | (num >> (32 - t));
        }

        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
