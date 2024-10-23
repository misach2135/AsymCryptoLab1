using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGenerators
{
    public interface IGenerator
    {
        BitArray GenBits(long seed, int length);
    }
}
