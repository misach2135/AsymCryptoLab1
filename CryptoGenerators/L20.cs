using CryptoGenerators;
using System;
using System.Collections;
using System.Collections.Generic;

public class L20Generator : IGenerator
{
    private BitArray register;
    private int period;

    public L20Generator()
    {
        register = new BitArray(20);

        period = (1 << 20) - 1;
    }

    public void Initialize(string initialValues)
    {
        if (initialValues.Length != 20)
        {
            throw new ArgumentException("initial values string must be 20 bits long");
        }

        for (int i = 0; i < 20; i++)
        {
            register[i] = initialValues[i] == '1';
        }
    }
    public bool NextBit()
    {
        bool nextBit = register[2] ^ register[4] ^ register[8] ^ register[19];

        for (int i = 19; i > 0; i--)
        {
            register[i] = register[i - 1];
        }

        register[0] = nextBit;

        return nextBit;
    }

    public BitArray GenBits(uint seed, int length)
    {
        Initialize(Convert.ToString(seed, 2).PadLeft(20, '0'));
        BitArray sequence = new(length);

        for (int i = 0; i < length; i++)
        {
            sequence[i] = NextBit();
        }

        return sequence;
    }
}