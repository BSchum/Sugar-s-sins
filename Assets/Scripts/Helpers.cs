using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Helpers
{
    /*
        * ComputeRatio(10, 1, 5) return 0,5;
        */
    public static float ComputeRatio(float maxA, float maxB, float currentValue, bool inverse = false)
    {
        float result = currentValue / maxA * maxB;
        if (inverse)
            return maxB - result;
        else
            return result;
    }
}

