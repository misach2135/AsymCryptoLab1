using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using CryptoGenerators;
using CryptoGenerators.Seed;
using CryptoGenerators.Tests;

namespace PRNGAnalyzer
{
    internal class Program
    {
        static List<IGenerator> generators = [];
        static RandomSequenceTester.TestsResult TestGenerator(IGenerator generator, int len, double alpha, long seed)
        {
            var byteArr = generator.GenBytes(seed, len);
            RandomSequenceTester tester = new();
            return tester.PerformTest(byteArr, alpha, 10);
        }

        static void AnalyzeGenerators(string outputFile, int r = 1, int len = 100_000)
        {
            using (StreamWriter sw = new(outputFile))
            {
                foreach (var g in generators)
                {
                    Console.WriteLine("{0}", g.GetType().Name);
                    sw.WriteLine("{0}: ", g.GetType().Name);

                    if (g.GetType() == typeof(LehmerGenerator))
                    {
                        sw.WriteLine("LehmerMode: {0}", (g as LehmerGenerator).Mode);
                    }

                    byte isProbabilityEquals = 0;
                    byte isCharIndependence = 0;
                    byte isUniform = 0;

                    for (int i = 0; i < r; i++)
                    {
                        var testRes = TestGenerator(g, len, 0.05, Seed.FromSystemTime());
                        isProbabilityEquals += testRes.isProbabilityEquals;
                        isCharIndependence += testRes.isCharIndependence;
                        isUniform += testRes.isUniform;
                    }

                    sw.WriteLine("IsProbabilityEquals: {0}", isProbabilityEquals / (double)r);
                    sw.WriteLine("IsCharIndependence:  {0}", isCharIndependence / (double)r);
                    sw.WriteLine("IsUniform:           {0}", isUniform / (double)r);
                    sw.WriteLine("\n");
                }
            }
        }

        static void Main(string[] args)
        {
            #region ListOfGenerators
            // List of generators
            generators.Add(new NativeGenerator());
            generators.Add(new LehmerGenerator(LehmerGenerator.LehmerMode.Low));
            generators.Add(new LehmerGenerator(LehmerGenerator.LehmerMode.High));
            generators.Add(new L20Generator());
            generators.Add(new L89Generator());
            generators.Add(new GeffeGenerator());
            generators.Add(new LibrarianGenerator("librarianText.txt"));
            generators.Add(new WolframGenerator());
            generators.Add(new BBSGenerator());
            //generators.Add(new BMGenerator());
            #endregion

            AnalyzeGenerators("out.txt", 5, 1_000_000);

        }
    }
}
