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

            long r_i = seed;

            for (int i = 0; i < length; i++)
            {
                res[i] = (r_i % 2) == 1;
                r_i = BitWizzardyUtils.LeftCyclicShift(r_i) ^ (r_i | BitWizzardyUtils.RightCyclicShift(r_i));
            }
            
            return res;
        }
    }
}
