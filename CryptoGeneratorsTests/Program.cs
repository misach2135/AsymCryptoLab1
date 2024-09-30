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
            WolphramGenerator gen = new();
            TestGenerator(gen, 1000);
        }
    }
}
