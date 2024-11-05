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
    public class LFSR128
    {
        private UInt128 _state;
        private int[] _mask;
        private int _deg;

        public UInt128 State
        {
            get
            {
                return _state;
            }
            set
            {
                var stateMask = (((UInt128.One << _deg) - 1));
                _state = value & stateMask;
            }
        }

        public LFSR128(UInt128 state, int deg, int[] poly)
        {
            if (_deg > 128)
            {
                throw new Exception("Degree bigger then 127 is not supported.");
            }
            _deg = deg;
            State = state;
            _mask = new int[poly.Length];
            poly.CopyTo(_mask, 0);
            foreach (int index in poly)
            {
                if (index >= _deg)
                {
                    throw new Exception($"{GetType()}: polynom index greater than {deg}");
                }
            }
        }

        public override string ToString()
        {
            return _state.ToString();
        }

        public int Next()
        {
            int res = (int)(_state & UInt128.One);
            byte newBit = 0;

            for (int i = 0; i < _mask.Length; i++)
            {
                newBit ^= _state.GetBit(_mask[i]);
            }
            
            _state >>= 1;
            _state = _state | ((newBit & UInt128.One) << _deg - 1);
            
            return res;
        }
    }
}
