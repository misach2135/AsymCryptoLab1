using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGenerators
{
    // TODO: Make rnd bigint generator for this class
    // TODO: Try to make it faster(approximatley, it works 11 hours for 20_000_000 length
    public class BMGenerator : IGenerator
    {
        private readonly BigInteger p = BigInteger.Parse("0cea42b987c44fa642d80ad9f51f10457690def10c83d0bc1bcee12fc3b6093e3", System.Globalization.NumberStyles.AllowHexSpecifier);
        private readonly BigInteger a = BigInteger.Parse("05b88c41246790891c095e2878880342e88c79974303bd0400b090fe38a688356", System.Globalization.NumberStyles.AllowHexSpecifier);
        private readonly BigInteger q = BigInteger.Parse("0675215cc3e227d3216c056cfa8f8822bb486f788641e85e0de77097e1db049f1", System.Globalization.NumberStyles.AllowHexSpecifier);

        // uint may be vulnerability!!
        public BitArray GenBits(long seed, int length)
        {
            BitArray res = new(length);
            BigInteger t_prev = new(seed);  // aka t_0
            BigInteger t = new();
            for (int i = 0; i < length; i++)
            {
                t = BigInteger.ModPow(a, t_prev, p);
                if (t < q) res[i] = true;
                else res[i] = false;
                t_prev = t;
            }
            return res;
        }

    }
}
