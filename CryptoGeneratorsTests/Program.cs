using System;
using System.Numerics;
using CryptoGenerators;
using BitWizzardy;

namespace CryptoGeneratorsTests
{
    internal class Program
    {
        static void TestGenerator(IGenerator gen, int length)
        {
            var bitArr = gen.GenBits(Convert.ToUInt32(DateTime.Now.Millisecond), length);
            Console.WriteLine(BitWizzardyUtils.ToBitString(bitArr));
        }

        static void Main(string[] args)
        {
            WolphramGenerator wolphramGen = new();
            BMGenerator bMGenerator = new();
            BBSGenerator bMSGenerator = new();
        }
    }
}
