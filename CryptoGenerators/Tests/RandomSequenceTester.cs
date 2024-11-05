using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BitWizzardy;

namespace CryptoGenerators.Tests
{
    public class RandomSequenceTester
    {

        public record struct TestsResult(byte isProbabilityEquals, byte isCharIndependence, byte isUniform);

        private byte[] bytes = [];
        private int[] bytesCount = [];

        public RandomSequenceTester() => bytesCount = new int[256];

        public TestsResult PerformTest(byte[] seq, double alpha, int r = 16)
        {
            bytes = seq;
            CalcBytesCount();
            byte isProbabilityEquals = Convert.ToByte(IsProbabilityEquals(alpha));
            byte isCharIndependence = (byte)(Convert.ToByte(IsCharIndependence(alpha)));
            byte isUniform = (byte)(Convert.ToByte(IsUniform(alpha, r)));

            TestsResult res = new(isProbabilityEquals, isCharIndependence, isUniform);

            return res;
        }

        private void CalcBytesCount()
        {
            for (int i = 0; i < bytesCount.Length; i++) bytesCount[i] = 0;
            
            for (int i = 0; i < bytes.Length; i++)
            {
                bytesCount[bytes[i]]++;
            }
        }

        private bool IsUniform(double alpha, int r)
        {
            double chiSquare = 0;
            int m = bytes.Length / r;
            int[,] bytesInRsegment = new int[r, 256];

            for (int i = 0; i < r; i++)
            {
                for (int j = i * m; j < i * m + m; j++)
                {
                    bytesInRsegment[i, bytes[j]]++;
                }
            }

            for (int i = 0; i <= 255; i++)
            {
                for (int j = 0; j <= r - 1; j++)
                {
                    if (bytesCount[i] == 0) continue;
                    chiSquare += (double)(bytesInRsegment[j, i] * bytesInRsegment[j, i]) / (m * bytesCount[i]);
                }
            }

            chiSquare = bytes.Length * (chiSquare - 1);
            double chiSquareCritical = Math.Sqrt(2 * 255 * (r - 1)) * GetQuantile(1 - alpha) + 255 * (r - 1);

            Console.WriteLine("ChiSquare in IsUniform: {0}", chiSquare);
            Console.WriteLine("ChiSquareCritical in IsUniform: {0}", chiSquareCritical);


            return chiSquare < chiSquareCritical;
        }

        private bool IsCharIndependence(double alpha)
        {
            int[] bytePairsCount = new int[65536];
            int[] firstPlaceCount = new int[256];
            int[] secondPlaceCount = new int[256];
            double chiSquare = 0;
            int n = bytes.Length / 2;

            for (int i = 1; i < bytes.Length / 2; i++)
            {
                var shortFromByte = BitWizzardyUtils.ShortFromBytes(bytes[2 * i - 1], bytes[2 * i]);
                bytePairsCount[shortFromByte]++;
            }

            for (int i = 0; i <= 255; i++)
            {
                for (int j = 0; j <= 255; j++)
                {
                    int ind1 = BitWizzardyUtils.ShortFromBytes((byte)i, (byte)j);
                    int ind2 = BitWizzardyUtils.ShortFromBytes((byte)j, (byte)i);
                    firstPlaceCount[i] += bytePairsCount[ind1];
                    secondPlaceCount[i] += bytePairsCount[ind2];
                }
            }

            for (int i = 0; i <= 255; i++)
            {
                for (int j = 0; j <= 255; j++)
                {
                    int index = BitWizzardyUtils.ShortFromBytes((byte)i, (byte)j);
                    if (firstPlaceCount[i] == 0 || secondPlaceCount[j] == 0) continue;
                    chiSquare += (double)(bytePairsCount[index] * bytePairsCount[index]) / (firstPlaceCount[i] * secondPlaceCount[j]);
                }
            }

            chiSquare = n * (chiSquare - 1);

            Console.WriteLine("ChiSquare in IsCharIndependence: {0}", chiSquare);

            double chiSquareCritical = Math.Sqrt(2 * 255 * 255) * GetQuantile(1 - alpha) + 255 * 255;

            Console.WriteLine("ChiSquareCritical in IsCharIndependence: {0}", chiSquareCritical);

            return chiSquare <= chiSquareCritical;
        }

        private bool IsProbabilityEquals(double alpha)
        {
            double hiSquare = 0;
            double n_j = bytes.Length / 256;

            for (int i = 0; i < 256; i++)
            {
                hiSquare += (double)((bytesCount[i] - n_j) * (bytesCount[i] - n_j)) / n_j;
            }

            double doubleLsquared = Math.Sqrt(2 * 255);
            double hiSquareCritical = doubleLsquared * GetQuantile(1 - alpha) + 255;

            return hiSquare <= hiSquareCritical;
        }

        private double GetQuantile(double gamma)
        {
            switch (gamma)
            {
                case 0.95:
                    return 1.645;
                case 0.99:
                    return 2.326;
                case 0.9:
                    return 1.282;
                default:
                    throw new ArgumentException("Our implementation sucks( Supported gamma is: 0.95; 0.99; 0.9");
            }
        }
    }
}
