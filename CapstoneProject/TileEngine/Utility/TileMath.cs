using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileEngine.Utility
{
   public class TileMath
    {
        /// <summary>
        /// Finds the GCD of two numbers. (Recursively)
        /// </summary>
        public static int gcd(int a, int b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            if (a > b)
                return gcd(a % b, b);
            else
                return gcd(a, b % a);
        }

    }
}
