using BitWizzardy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGenerators
{

    public class LibrarianGenerator : IGenerator
    {
        byte[] _data = [];

        public LibrarianGenerator(string filename)
        {
            SetSource(filename);
        }

        public void SetSource(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(string.Format("{0} cannot be found.", filename));
            }
            List<byte> bytes = [];

            using (StreamReader reader = new(filename, Encoding.ASCII))
            {
                while (!reader.EndOfStream)
                {
                    bytes.Add((byte)reader.Read());
                }
            }

            _data = bytes.ToArray();
        }

        public BitArray GenBits(long seed, int length)
        {
            BitArray res = new(length);
            BitArray bitsData = new BitArray(_data);
            seed = Convert.ToUInt32(seed % bitsData.Count);
            for (int i = 0; i < length; i++)
            {
                res[i] = bitsData[Convert.ToInt32(seed + i) % bitsData.Count];
            }
            return res;
        }

        public byte[] GenBytes(long seed, int length)
        {
            byte[] res = new byte[length];
            for (int i = 0; i < length; i++)
            {
                res[i] = _data[Convert.ToInt32((seed + i) % _data.Length)];
            }
            return res;
        }
    }
}
