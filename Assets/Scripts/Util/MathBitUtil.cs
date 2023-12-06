using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horizon
{
    public static class MathBitUtil
    {
        public static bool BitTrueInt(int value, int index)
        {
            bool result = ((1 << index) & value) > 0;
            return result;
        }
        public static bool BitTrueLong(long value, int index)
        {
            bool result = ((1 << index) & value) > 0;
            return result;
        }


        public static int BitInt_Or(int value, int index)
        {
            value = ((1 << index) | value);
            return value;
        }
        public static int BitInt_And(int value, int index)
        {
            value = ((1 << index) & value);
            return value;
        }
    }
}

