using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using BitWizzardy;
using System.Diagnostics;

namespace CryptoGenerators.Tests
{
    public static class RandomSequenceTester
    {
        static public int[] GetBytesCount(byte[] seq)
        {
            int[] bytesCount = new int[256];

            for (int i = 0; i < seq.Length; i++)
            {
                bytesCount[seq[i]]++;
            }
            return bytesCount;
        }

        static public (double, double) CheckEqualProbability(byte[] seq, double alpha)
        {
            int[] bytesCount = new int[256];

            for (int i = 0; i < seq.Length; i++)
            {
                bytesCount[seq[i]]++;
            }

            double expectedBytes = seq.Length / 256.0;
            double chiSquare = 0;
            for (int i = 0; i < 256; i++)
            {
                chiSquare += (bytesCount[i] - expectedBytes) * (bytesCount[i] - expectedBytes) / expectedBytes;
            }
            double expectedChiSquare = Math.Sqrt(2 * 255) * Normal.InvCDF(0, 1, 1 - alpha) + 255;

            return (expectedChiSquare, chiSquare);
        }

        static public (double, double) CheckCharIndependence(byte[] seq, double alpha)
        {
            int[,] pairsCount = new int[256, 256];
            int[] firstPlaceCount = new int[256];
            int[] secondPlaceCount = new int[256];
            int n = seq.Length / 2;

            for (int i = 0; i < n; i++)
            {
                pairsCount[seq[2 * i], seq[2 * i + 1]]++;
            }

            int pairsSum = 0;
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    firstPlaceCount[i] += pairsCount[i, j];
                    secondPlaceCount[i] += pairsCount[j, i];
                    pairsSum += pairsCount[i, j];
                }
            }

            double chiSquare = 0;

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    if (firstPlaceCount[i] == 0 || secondPlaceCount[j] == 0) continue;
                    chiSquare += (pairsCount[i, j] * pairsCount[i, j]) / (double)(firstPlaceCount[i] * secondPlaceCount[j]);
                }
            }

            chiSquare = n * (chiSquare - 1);
            double expectedChiSquare = Math.Sqrt(2 * 255 * 255) * Normal.InvCDF(0, 1, 1 - alpha) + 255 * 255;
            Debug.Assert(chiSquare > 0);
            return (expectedChiSquare, chiSquare);
        }

        static public (double, double) CheckUniformity(byte[] seq, double alpha, int r)
        {
            int rSize = seq.Length / r;
            int n = rSize * r;
            int[,] byteInSegment = new int[r, 256];

            for (int i = 0; i < r; i++)
            {
                for (int j = i * rSize; j < i * rSize + rSize; j++)
                {
                    byteInSegment[i, seq[j]]++;
                }
            }

            double chiSquare = 0;

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    int byteCount = 0;
                    for (int t = 0; t < r; t++)
                    {
                        byteCount += byteInSegment[t, i];
                    }
                    if (byteCount == 0) continue;
                    chiSquare += (byteInSegment[j, i] * byteInSegment[j, i]) / (double)(byteCount * rSize);
                }
            }

            chiSquare = n * (chiSquare - 1);

            double expectedChiSquare = Math.Sqrt(2 * 255 * (r - 1)) * Normal.InvCDF(0, 1, 1 - alpha) + 255 * (r - 1);
            return (expectedChiSquare, chiSquare);
        }

    }
}
