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
            generators.Add(new WolframGenerator());
            //generators.Add(new BMGenerator());
            //generators.Add(new BBSGenerator());

            PrintAllGeneratorsInFile("out.txt", 1_000_000);
            #endregion


            var wolf_gen = new WolframGenerator();
            var res = wolf_gen.GenBits(Seed.FromSystemTime(), 200);

            Console.WriteLine(res.ToBitString());


        }
    }
}
