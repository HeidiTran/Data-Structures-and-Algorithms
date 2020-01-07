using System;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 20;
            int res = Factorial(N);
            Console.WriteLine(N + "! = " + res);
        }

        private static int Factorial(int N)
        {
            if (N == 0)
                return 1;

            return N * Factorial(N - 1);
        }
    }
}
