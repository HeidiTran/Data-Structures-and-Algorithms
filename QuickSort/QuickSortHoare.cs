using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    class Program
	{
		static void Main(string[] args)
		{
            ExampleQuickSort();
        }

        private static int PartitionHoareScheme(int[] A, int l, int r)
        {
            // Choosing a random pivot minimizes the chance that you encounter worst case O(n^2) performance. 
            // Always choosing the first or the last would cause worst-case performance for 
            // nearly-sorted or nearly-reversed-sorted
            int pivotIndex = new Random().Next(l, r);
            Swap(ref A[l], ref A[pivotIndex]);

            int pivot = A[l];

            // Hoare's partition scheme is used here: The indices i & j run towards each other. When a "misplaced"
            // pair is found (a large element currently located in the left part and a small element located 
            // in the right part) perform a swap
            int i = l + 1;
            int j = r - 1;
            while (true)
            {
                // Find item on left side to swap
                while (i < r - 1 && A[i] <= pivot)
                    i++;

                // Find item on right side to swap
                while (j > l && pivot < A[j])
                    j--;

                // check if pointers cross
                if (i >= j)
                    break;

                Swap(ref A[i], ref A[j]);
            }

            // Put pivot at j position
            Swap(ref A[l], ref A[j]);
            return j;
        }

        private static void QuickSort(int[] A, int l, int r)
        {
            if (r - 1 <= l)
                return;

            int m = PartitionHoareScheme(A, l, r);
            QuickSort(A, l, m);
            QuickSort(A, m + 1, r);
        }


        private static void ExampleQuickSort()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            QuickSort(A, 0, A.Length);

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
