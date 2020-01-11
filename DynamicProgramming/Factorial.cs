using System;
using System.Collections.Generic;

namespace CSharpSample 
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 30;
            Console.WriteLine("Factorial(" + N + ") = " + Factorial(N));
        }

        private static long[] knownFact = new long[200];
        private static long Factorial(long n)
        {
            if (n <= 1)
                knownFact[n] = 1;
            else if (knownFact[n] == 0)
                knownFact[n] = n * Factorial(n - 1);

            return knownFact[n];
        }
    }
}
