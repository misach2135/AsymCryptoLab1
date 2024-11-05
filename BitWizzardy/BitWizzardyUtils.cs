    using System;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text;

namespace BitWizzardy
{
    public static class BitWizzardyUtils
    {
        public static long RightCyclicShift(this long num, int t = 1)
        {
            return (num >>> t) | (num << (64 - t));
        }

        public static long LeftCyclicShift(this long num, int t = 1)
        {
            return (num << t) | (num >>> (64 - t));
        }

        public static uint RightCyclicShift(this uint num, int t = 1)
        {
            return (num >> t) | (num << (32 - t));
        }

        public static uint LeftCyclicShift(this uint num, int t = 1)
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

        public static byte[] ToByteArray(this long num)
        {
            throw new NotImplementedException();
            return null;
        }

        public static int GetBit(this Vector128<int> vector, int i)
        {
            int blockId = i / 32;
            i = i % 32;
            return (vector[blockId] & (1 << i)) >> i;
        }

        public static byte GetBit(this UInt128 num, int i)
        {
            return (byte)((num & (UInt128.One << i)) >> i);
        }

    }
}
