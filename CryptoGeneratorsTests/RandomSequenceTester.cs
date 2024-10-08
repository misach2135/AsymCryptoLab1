using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BitWizzardy;

namespace CryptoGeneratorsTests
{
    class RandomSequenceTester
    {
        private byte[] bytes;
        private int[] bytesCount;
        private int[] bytePairsCount;

        public RandomSequenceTester(byte[] sequence)
        {
            bytes = sequence;
            bytesCount = new int[256];
            bytePairsCount = new int[65536];

            for (int i = 0; i < bytesCount.Length; i++) bytesCount[i] = 0;
            for (int i = 0; i < bytePairsCount.Length; i++) bytePairsCount[i] = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                bytesCount[bytes[i]]++;
            }

            for (int i = 0; i < bytes.Length / 2; i++)
            {
                bytePairsCount[BitWizzardyUtils.ShortFromBytes(bytes[2 * i - 1], bytes[2 * i])]++;
            }
        }

        public void PerformTest()
        {

        }

        private bool IsUniform()
        {
            return false;
        }

        private bool IsCharIndependence()
        {
            return false;
        }

        private bool IsProbabilityEquals()
        {
            return false;
        }

    }
}
