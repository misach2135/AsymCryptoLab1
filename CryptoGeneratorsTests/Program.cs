using System;
using System.Numerics;
using CryptoGenerators;

namespace CryptoGeneratorsTests
{
    internal class Program
    {
        static void TestGenerator(IGenerator gen, int length)
        {
            var bitArr = gen.GenBits(Convert.ToUInt32(DateTime.Now.Millisecond), length);
            Console.WriteLine(Utils.ToBitString(bitArr));
        }

        static void Main(string[] args)
        {
            WolphramGenerator wolphramGen = new();
            BMGenerator bMGenerator = new();
            BMSGenerator bMSGenerator = new();
            Console.WriteLine("Round 1: ");
            TestGenerator(bMSGenerator, 1000);
            Console.WriteLine("Round 2: ");
            TestGenerator(bMSGenerator, 1000);
            Console.WriteLine("Round 3: ");
            TestGenerator(bMSGenerator, 1000);
            Console.WriteLine("Round 4: ");
            TestGenerator(bMSGenerator, 1000);
            Console.WriteLine("Round 5: ");
            TestGenerator(bMSGenerator, 1000);
            Console.WriteLine("Round 6: ");
            TestGenerator(bMSGenerator, 1000);
        }
    }
}
