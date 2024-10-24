using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Runtime.Intrinsics;
using System.Threading.Tasks;

namespace BitWizzardy
{

    // This is not production-ready implementation, and using it very carefully.
    // Will be better to upgrade this impl)
    // Finded issues:
    // 1. initState is not checked for size, whicj is important for state, becouse can break up state
    // 2. ...

    public class LFSR
    {
        private Vector128<int> state;
        private int[] mask;
        private int deg;

        public LFSR(int[] initState, int deg, int[] poly)
        {
            state = Vector128.Create(initState);
            mask = new int[poly.Length];
            poly.CopyTo(mask, 0);
            this.deg = deg;
            foreach (int index in poly)
            {
                if (index >= deg)
                {
                    throw new Exception($"{GetType()}: polynom index greater than {deg}");
                }
            }
        }

        public void SetState(int[] initState)
        {
            state = Vector128.Create(initState);
        }

        public override string ToString()
        {
            return state.ToString();
        }

        public int Next()
        {
            int res = (state & Vector128<int>.One).ToScalar();
            int newBit = 0;

            for (int i = 0; i < mask.Length; i++)
            {
                newBit ^= state.GetBit(mask[i]);
            }
            
            state >>= 1;
            state = (Vector128.CreateScalar(newBit) << deg - 1) | state;
            
            return res;
        }
    }
}
