using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample 
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleCountingSort();
        }

        private static int[] CountingSort(int[] A)
        {
            // Time complexity is O(n + k) with k is the range of the input
            int[] sorted = new int[A.Length];
            int k = GetMaxElem(A, 0, A.Length);
            int[] counter = new int[k + 1];

            // Increase the counter by number of occurrence of obj
            for (int i = 0; i < A.Length; i++)
                counter[A[i]]++;

            // Update counter array to contains actual positions in the output 
            for (int i = 1; i <= k; i++)
                counter[i] += counter[i - 1];

            // Do it in reverse order to make counting sort stable
            for (int i = A.Length - 1; i >= 0; i--)
            {
                // counter[A[i]] - 1 is the index position of A[i] after sorted
                sorted[counter[A[i]] - 1] = A[i];
                counter[A[i]]--;
            }

            return sorted;
        }

        private static int GetMaxElem(int[] A, int l, int r)
        {
            int max = int.MinValue;
            for (int i = l; i < r; i++)
                if (A[i] > max)
                    max = A[i];

            return max;
        }

        private static void ExampleCountingSort()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            int[] C = CountingSort(A);

            Console.WriteLine("\nSorted:");
            foreach (var item in C)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
