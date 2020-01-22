using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleQuickSortLomuto();
        }

        private static int PartitionLomutoScheme(int[] A, int l, int r)
        {
            // Lomuto's partition scheme: The index variable j scans the whole array and whenever we find an element
            // A[j] smaller than the pivot we do a swap

            int pivot = A[r - 1];

            int i = l - 1;
            for (int j = l; j < r - 1; j++)
            {
                if (A[j] < pivot)
                {
                    i++;
                    Swap(ref A[i], ref A[j]);
                }
            }

            Swap(ref A[i + 1], ref A[r - 1]);
            return i + 1;
        }

        private static void QuickSortLomuto(int[] A, int l, int r)
        {
            if (r - 1 <= l)
                return;

            int m = PartitionLomutoScheme(A, l, r);
            QuickSortLomuto(A, l, m);
            QuickSortLomuto(A, m + 1, r);
        }

        private static void ExampleQuickSortLomuto()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);
            
            foreach (var item in A)
                Console.Write(item + " ");

            QuickSortLomuto(A, 0, A.Length);

            Console.WriteLine("\nSorted:");
            foreach (var item in A)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}
