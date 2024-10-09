using CryptoGenerators;
using System;
using System.Collections;


public class L89Generator : IGenerator
{
    private BitArray register;
    private int period;

    public L89Generator()
    {
        register = new BitArray(89);

        period = (1 << 89) - 1;
    }

    public void Initialize(string initialValues)
    {
        if (initialValues.Length != 89)
        {
            throw new ArgumentException("initial values string must be 89 bits long");
        }

        for (int i = 0; i < 89; i++)
        {
            register[i] = initialValues[i] == '1';
        }
    }
    public bool NextBit()
    {
        bool nextBit = register[38] ^ register[89];

        for (int i = 88; i > 0; i--)
        {
            register[i] = register[i - 1];
        }

        register[0] = nextBit;

        return nextBit;
    }

    public BitArray GenBits(uint seed, int length)
    {
        Initialize(Convert.ToString(seed, 2).PadLeft(89, '0'));
        BitArray sequence = new(length);

        for (int i = 0; i < length; i++)
        {
            sequence[i] = NextBit();
        }

        return sequence;
    }
}