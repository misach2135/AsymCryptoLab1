using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BitWizzardy;

namespace CryptoGenerators
{
    public class WolframGenerator : IGenerator
    {
        public BitArray GenBits(long seed, int length)
        {
            BitArray res = new BitArray(length);

            uint r_i = Convert.ToUInt32(seed % (1U << 31));

            for (int i = 0; i < length; i++)
            {
                res[i] = (r_i % 2) == 1;
                r_i = r_i.LeftCyclicShift() ^ (r_i | r_i.RightCyclicShift());
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
