using System;

namespace CSharpSample
{
    class Program
    {
        public static void InsertionSort(int[] A)
        {
            // The idea: All the elems to the left of the current index are in sorted order
            // Make space for the elem being inserted by moving larger elem 1 pos to the right
            // and insert the elem in the vacated pos
            for (int itemIndex = 1; itemIndex < A.Length; itemIndex++)
            {
                int itemValue = A[itemIndex];
                int j = itemIndex - 1;

                while (j >= 0 && itemValue <= A[j])
                {
                    A[j + 1] = A[j];
                    j -= 1;
                }

                A[j + 1] = itemValue;
            }
        }

        private static void ExampleInsertionSort()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            InsertionSort(A);

            Console.WriteLine("\nSorted:");
            foreach (var item in A)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            ExampleInsertionSort();
        }
    }
}
