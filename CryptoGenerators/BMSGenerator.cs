using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGenerators
{
    // TODO: find out chunking in the begining of the sequence
    public class BMSGenerator : IGenerator
    {
        private readonly BigInteger p = BigInteger.Parse("d5bbb96d30086ec484eba3d7f9caeb07", System.Globalization.NumberStyles.AllowHexSpecifier);
        private readonly BigInteger q = BigInteger.Parse("425d2b9bfdb25b9cf6c416cc6e37b59c1f", System.Globalization.NumberStyles.AllowHexSpecifier);
        private readonly BigInteger n;

        public BMSGenerator()
        {
            n = p * q;
        }

        public BitArray GenBits(uint seed, int length)
        {
            BitArray res = new(length);
            BigInteger r_prev = new(seed);
            BigInteger r = new();
            for (int i = 0; i < length; i++)
            {
                r = BigInteger.ModPow(r_prev, 2, n);
                res[i] = (r % 2) == 1;
                r_prev = r;
            }
            return res;
        }
    }
}
