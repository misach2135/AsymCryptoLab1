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
using MathNet.Numerics.Integration;
using MathNet.Numerics.Distributions;
using System.Text;
using System.Diagnostics.Tracing;

namespace CryptoGeneratorsTests
{
    internal class Program
    {

        // seqLen in bytes
        // outputs results of tests
        static void AnalyzeGenerators1(string outPath, IGenerator[] generators, int seqLen, double alpha, int r)
        {
            Console.WriteLine("seqLen: {0}, alpha: {1}, r: {2}", seqLen, alpha, r);
            string[,] csvTable = new string[4, generators.Length + 1];
            csvTable[0, 0] = "Tests";
            csvTable[1, 0] = "ProbabilityEquals";
            csvTable[2, 0] = "CharIndependence";
            csvTable[3, 0] = "Uniformity";
            for (int i = 0; i < generators.Length; i++)
            {
                csvTable[0, i + 1] = generators[i].GetType().Name;
                var seq = generators[i].GenBytes(Seed.FromSystemTime(), seqLen);
                var probabilityEquals = RandomSequenceTester.CheckEqualProbability(seq, alpha);
                var charIndependence = RandomSequenceTester.CheckCharIndependence(seq, alpha);
                var uniformity = RandomSequenceTester.CheckUniformity(seq, alpha, r);
                csvTable[1, i + 1] = Convert.ToString(probabilityEquals.Item1 - probabilityEquals.Item2 > 0 ? "1" : "0");
                csvTable[2, i + 1] = Convert.ToString(charIndependence.Item1 - charIndependence.Item2 > 0 ? "1" : "0");
                csvTable[3, i + 1] = Convert.ToString(uniformity.Item1 - uniformity.Item2 > 0 ? "1" : "0");
            }

            using (StreamWriter sw = new(outPath, false, new UTF8Encoding()))
            {
                sw.WriteLine(StringArrToCSVText(csvTable));
            }
        }

        // Output chiSquare values
        static void AnalyzeGenerators2(string outPath, IGenerator[] generators, int seqLen, double alpha, int r)
        {
            Console.WriteLine("seqLen: {0}, alpha: {1}, r: {2}", seqLen, alpha, r);
            string[,] csvTable = new string[7, generators.Length + 2];
            csvTable[0, 0] = "Tests";
            csvTable[0, 1] = "chiSquares";
            csvTable[1, 0] = "ProbabilityEquals";
            csvTable[1, 1] = "ExpectedChiSquare";
            csvTable[2, 1] = "ChiSquare";
            csvTable[3, 0] = "CharIndependence";
            csvTable[3, 1] = "ExpectedChiSquare";
            csvTable[4, 1] = "ChiSquare";
            csvTable[5, 0] = "Uniformity";
            csvTable[5, 1] = "ExpectedChiSquare";
            csvTable[6, 1] = "ChiSquare";
            for (int i = 0; i < generators.Length; i++)
            {
                int t = i + 2;
                csvTable[0, t] = generators[i].GetType().Name;
                var seq = generators[i].GenBytes(Seed.FromSystemTime(), seqLen);
                var probabilityEquals = RandomSequenceTester.CheckEqualProbability(seq, alpha);
                var charIndependence = RandomSequenceTester.CheckCharIndependence(seq, alpha);
                var uniformity = RandomSequenceTester.CheckUniformity(seq, alpha, r);
                csvTable[1, t] = probabilityEquals.Item1.ToString();
                csvTable[2, t] = probabilityEquals.Item2.ToString();
                csvTable[3, t] = charIndependence.Item1.ToString();
                csvTable[4, t] = charIndependence.Item2.ToString();
                csvTable[5, t] = uniformity.Item1.ToString();
                csvTable[6, t] = uniformity.Item2.ToString();

            }

            using (StreamWriter sw = new(outPath, false, new UTF8Encoding()))
            {
                sw.WriteLine(StringArrToCSVText(csvTable));
            }
        }

        static string StringArrToCSVText(string[,] arr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    sb.Append(arr[i, j]);
                    sb.Append(';');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        static void TestSeq(byte[] seq)
        {

        }

        //static void PrintAllGeneratorsInFile(string path, int len)
        //{
        //    using (StreamWriter writer = new StreamWriter(path))
        //    {
        //        foreach (var g in generators)
        //        {
        //            writer.WriteLine("Generator {0}: ", g.GetType().Name);
        //            if (g is LehmerGenerator)
        //            {
        //                writer.WriteLine("LehmerType: {0}", (g as LehmerGenerator).Mode);
        //            }
        //            var bits = g.GenBits(Seed.FromSystemTime(), len);
        //            writer.WriteLine(bits.ToBitString());
        //            writer.WriteLine("\n");
        //        }
        //    }
        //}

        static void Main(string[] args)
        {
            List<IGenerator> generators = [];
            double[] alphaArr = [0.01, 0.05, 0.1];
            int[] seqLength = [500_000, 1_000_000, 5_000_000];
            #region ListOfGenerators
            // List of generators
            //generators.Add(new NativeGenerator());
            //generators.Add(new LehmerGenerator(LehmerGenerator.LehmerMode.Low));
            //generators.Add(new LehmerGenerator(LehmerGenerator.LehmerMode.High));
            generators.Add(new L20Generator());
            //generators.Add(new L89Generator());
            //generators.Add(new GeffeGenerator());
            //generators.Add(new LibrarianGenerator("librarianText.txt"));
            //generators.Add(new WolframGenerator());
            //generators.Add(new BBSGenerator());
            // generators.Add(new BMGenerator());

            //PrintAllGeneratorsInFile("out.txt", 1_500_000);
            #endregion

            //foreach(var alpha in alphaArr)
            //{
            //    string fileName = "Analyzer1Results_" + Convert.ToString(alpha).Replace(",", String.Empty) + ".csv";
            //    AnalyzeGenerators2(fileName, generators.ToArray(), 1_000_000, alpha, 5);
            //}
            AnalyzeGenerators2("res1.csv", generators.ToArray(), 125_000, 0.1, 5);
            AnalyzeGenerators2("res2.csv", generators.ToArray(), 1_000_000, 0.1, 5);

        }
    }
}
