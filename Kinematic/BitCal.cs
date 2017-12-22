using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinematic
{
    internal static class BitCal
    {
        public static uint BitCount(uint value)
        {
            uint tmp = value - ((value >> 1) & 0x55555555);
            tmp = (tmp & 0x33333333) + ((tmp >> 2) & 0x33333333);
            return (((tmp + (tmp >> 4)) & 0x0f0f0f0f) * 0x01010101) >> 24;
        }
    }
}
