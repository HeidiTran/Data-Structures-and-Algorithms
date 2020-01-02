using System;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            FindPrimeNumbers();
        }

        private static void FindPrimeNumbers()
        {
            // The goal of this program is to set A[i] to 1 if i is prime, and to 0 otherwise
            const int N = 1000;
            int[] A = new int[N];

            for (int i = 2; i < N; i++)
                A[i] = 1;

            for (int i = 2; i < N; i++)
            {
                if (A[i] == 1)
                {
                    for (int j = i; j * i < N; j++)
                        A[i * j] = 0;
                }
            }

            for (int i = 2; i < N; i++)
            {
                if (A[i] == 1)
                    Console.WriteLine(i);
            }
        }
    }
}
