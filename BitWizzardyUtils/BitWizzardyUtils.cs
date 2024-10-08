using System;
using System.Collections;
using System.Text;

namespace BitWizzardy
{
    public static class BitWizzardyUtils
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

        public static ushort ShortFromBytes(byte b1, byte b2) => (ushort)((b1) | (b2 << 8));
    }
}
