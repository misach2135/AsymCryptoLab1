using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoGenerators.Seed
{
    public static class Seed
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        public static long FromMouseMovement(int k = 10)
        {
            Point p1;
            long res = 0;

            for (int i = 0; i < k; i++)
            {
                bool err = GetCursorPos(out p1);
                if (!err)
                {
                    Console.WriteLine("Warning! Error while getting input from mouse");
                }

                res += (p1.X - p1.Y) * (p1.X - p1.Y);

                Thread.Sleep(20);
            }

            res /= k;

            return res;
        }

        public static long FromSystemTime()
        {
            return DateTime.Now.Microsecond * (DateTime.Now.Ticks >>> 10);
        }
    }
}
