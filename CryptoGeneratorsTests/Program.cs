using System;
using System.IO;
using TrulyRandom;
using CryptoGenerators;
using CryptoGenerators.Tests;
using CryptoGenerators.Seed;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using BitWizzardy;
using System.Collections.Generic;

namespace CryptoGeneratorsTests
{
    internal class Program
    {
        static List<IGenerator> generators = [];

        static void TestRandomnessTests(byte[] inputBytes)
        {
        }

        static void PrintAllGeneratorsInFile(string path, int len)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var g in generators)
                {
                    writer.WriteLine("Generator {0}: ", g.GetType().Name);
                    if (g is LehmerGenerator)
                    {
                        writer.WriteLine("LehmerType: {0}", (g as LehmerGenerator).Mode);
                    }
                    var bits = g.GenBits(Seed.FromSystemTime(), len);
                    writer.WriteLine(bits.ToBitString());
                    writer.WriteLine("\n");
                }
            }
        }

        static void Main(string[] args)
        {
            #region ListOfGenerators
            // List of generators
            //generators.Add(new NativeGenerator());
            //generators.Add(new LehmerGenerator(LehmerGenerator.LehmerMode.Low));
            //generators.Add(new LehmerGenerator(LehmerGenerator.LehmerMode.High));
            //generators.Add(new L20Generator());
            //generators.Add(new L89Generator());
            //generators.Add(new GeffeGenerator());
            //generators.Add(new LibrarianGenerator("librarianText.txt"));
            //generators.Add(new WolframGenerator());
            //generators.Add(new BMGenerator());
            //generators.Add(new BBSGenerator());

            //PrintAllGeneratorsInFile("out.txt", 1_500_000);
            #endregion

            LibrarianGenerator gen = new("librarianText.txt");

            var seed = Seed.FromSystemTime();
            var bytes = gen.GenBytes(seed, 10_000_000);
            var res = RandomSequenceTester.CheckUniformity(bytes, 0.1, 1000);
            //var testRes = RandomSequenceTester.CheckUniformity([], 0.1, 100);
            var bytesCount = RandomSequenceTester.GetBytesCount(bytes);
            Console.WriteLine("Seed: {0}", seed);
            //Console.WriteLine("Bits: {0}", string.Join(',', bytes));
            Console.WriteLine("Res: {0}", res);

            //for (int i = 0; i < 256; i++)
            //{
            //    Console.WriteLine("{0} == {1}", i, bytesCount[i]);
            //}

            Console.WriteLine("Min: {0}", bytesCount.Min());
            Console.WriteLine("Max: {0}", bytesCount.Max());
            Console.WriteLine("Delta: {0}", bytesCount.Max() - bytesCount.Min());
            //Console.WriteLine("TestRes: {0}", testRes);

        }
    }
}
