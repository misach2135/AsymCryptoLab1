using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CryptoGenerators;
using CryptoGenerators.Seed;
using CryptoGenerators.Tests;

namespace CryptoGeneratorsAnalyzer
{
    internal class Program
    {

        static int TestGenerator(IGenerator generator, int len, long seed)
        {
            var bitArray = generator.GenBits(seed, len);
            byte[] testByteArr = new byte[len / 8];
            bitArray.CopyTo(testByteArr, 0);
            RandomSequenceTester tester = new();
            return tester.PerformTest(testByteArr, 0.1);
        }

        static void Main(string[] args)
        {
            string librarianIn = args.Length > 0 ? args[0] : "librarianText.txt";
            IGenerator[] generators = [new NativeGenerator(),
                new L20Generator(), new L89Generator(), new LibrarianGenerator(librarianIn), new WolframGenerator(), new BBSGenerator(), new BMGenerator()];

            foreach (var gen in generators)
            {
                Console.WriteLine($"{gen.GetType().Name}:\n");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{TestGenerator(gen, 5_000_000, Seed.FromSystemTime())}");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
