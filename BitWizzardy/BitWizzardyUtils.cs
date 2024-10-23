using System;
using System.Collections;
using System.Text;

namespace BitWizzardy
{
    public static class BitWizzardyUtils
    {
        public static long RightCyclicShift(long num, int t = 1)
        {
            return (num >>> t) | (num << (64 - t));
        }

        public static long LeftCyclicShift(long num, int t = 1)
        {
            return (num << t) | (num >>> (64 - t));
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

        public static byte[] ToByteArray(this long num)
        {
            throw new NotImplementedException();
            return null;
        }
    }
}
