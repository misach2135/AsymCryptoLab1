using System;
using System.Numerics;
using CryptoGenerators;
using BitWizzardy;
using System.IO;

namespace CryptoGeneratorsTests
{
    internal class Program
    {
        static void TestGenerator(IGenerator gen, int length)
        {
            var bitArr = gen.GenBits(Convert.ToUInt32(DateTime.Now.Millisecond), length);
            //Console.WriteLine(BitWizzardyUtils.ToBitString(bitArr));

            byte[] bytes = new byte[bitArr.Length / 8];

            bitArr.CopyTo(bytes, 0);

            //Console.WriteLine(Convert.ToHexString(bytes));

            RandomSequenceTester rndTest = new();
            Console.WriteLine(Convert.ToString(rndTest.PerformTest(bytes, 0.01), 2));
        }

        static void Main(string[] args)
        {
            WolframGenerator wolframGen = new();
            BMGenerator bMGenerator = new();
            BBSGenerator bMSGenerator = new();

            for (int i = 0; i < 10; i++)
            {
                TestGenerator(bMGenerator, 20_000_000);
            }
        }
    }
}
