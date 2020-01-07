using System;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleCocktailSort();
        }

        static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static void CocktailSort(int[] A)
        {
            // Cocktail Sort or Shaker Sort is an extension version of Bubble Sort
            int l = 0, r = A.Length - 1;

            for (int i = l; i < r; i++)
            {
                for (int j = r; j > i; j--)
                {
                    if (A[i] > A[i + 1])
                    {
                        Swap(ref A[i], ref A[i + 1]);
                    }

                    if (A[j] < A[j - 1])
                    {
                        Swap(ref A[j], ref A[j - 1]);
                    }
                }
            }
        }

        private static void ExampleCocktailSort()
        {
            const int N = 6;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            CocktailSort(A);

            Console.WriteLine("\nSorted:");
            foreach (var item in A)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
