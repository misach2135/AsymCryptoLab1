using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGenerators
{
    public class WolphramGenerator : IGenerator
    {
        public BitArray GenBits(uint seed, int length)
        {
            BitArray res = new BitArray(length);

            uint r_i = seed;

            for (int i = 0; i < length; i++)
            {
                res[i] = (r_i % 2) == 1;
                r_i = Utils.LeftCyclicShift(r_i) ^ (r_i | Utils.RightCyclicShift(r_i));
            }
            
            return res;
        }
    }
}
