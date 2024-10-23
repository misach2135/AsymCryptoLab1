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
        BitArray data;
        
        public LibrarianGenerator(string filename)
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

            data = new BitArray(bytes.ToArray());
        }

        public BitArray GenBits(long seed, int length)
        {
            BitArray res = new(length);
            seed = Convert.ToUInt32(seed % data.Count);
            for (int i = 0; i < length; i++)
            {
                res[i] = data[Convert.ToInt32(seed + i) % data.Count];
            }
            return res;
        }
    }
}
