using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGenerators
{
    // Would be better to make it as abstract class maybe, imo
    // Then all methods would have defalut impl, based on other.
    // So, it would be neccesary to impl only one method
    // (but how would we control, that minimum one method implemented?)
    public interface IGenerator
    {
        BitArray GenBits(long seed, int length);
        byte[] GenBytes(long seed, int length);
    }
}
