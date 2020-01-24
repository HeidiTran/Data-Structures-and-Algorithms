using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleQuicksort3WaysFast();
        }

        private static Tuple<int, int> Partion3WaysFast(int[] A, int l, int r)
        {
            // This is Bentley-Mclroy's 3-way partitioning scheme
            // Indices p and q such that A[lo..p-1] = A[q+1..hi] = pivot, 
            // an index i such that A[p..i-1] < pivot and an index j such that A[j+1..q] > pivot
            int i = l, j = r;
            int p = l, q = r;
            int pivot = A[l];
            while (true)
            {
                while (A[++i] < pivot)
                    if (i == r - 1) break;

                while (pivot < A[--j])
                    if (j == l) break;

                if (i == j && A[i] == pivot)
                    Swap(ref A[++p], ref A[i]);

                if (i >= j)
                    break;

                Swap(ref A[i], ref A[j]);
                if (A[i] == pivot)
                    Swap(ref A[++p], ref A[i]);

                if (A[j] == pivot)
                    Swap(ref A[--q], ref A[j]);
            }

            i = j + 1;
            while (p >= l)
                Swap(ref A[p--], ref A[j--]);

            while (q <= r - 1)
                Swap(ref A[q++], ref A[i++]);

            // A[l..j] < pivot and A[i..r-1] > pivot
            return new Tuple<int, int>(j + 1, i);
        }

        private const int INSERTION_SORT_CUTOFF = 27;
        private static void QuickSort3WaysFast(int[] A, int l, int r)
        {
            // Classic implementation
            //if (r - 1 <= l)
            //   return;

            // For small array, Use Insertion sort
            if (r - l <= INSERTION_SORT_CUTOFF)
            {
                InsertionSort(A, l, r);
                return;
            }

            // Use median-of-3 partitioning elem
            int m = MedianOf3(A, l, (l + r - 1) / 2, r - 1);
            Swap(ref A[l], ref A[m]);

            Tuple<int, int> pair = Partion3WaysFast(A, l, r);
            QuickSort3WaysFast(A, l, pair.Item1);
            QuickSort3WaysFast(A, pair.Item2, r);
        }


        // Return the index of the median element between A[i], A[j], A[k]
        private static int MedianOf3(int[] A, int i, int j, int k)
        {
            return (A[i] < A[j]) ?
               (A[j] < A[k]) ? j : (A[i] < A[k]) ? k : i :
               (A[k] < A[j]) ? j : (A[k] < A[i]) ? k : i;
        }

        private static void InsertionSort(int[] A, int l, int r)
        {
            for (int itemIndex = l; itemIndex < r; itemIndex++)
            {
                int itemValue = A[itemIndex];
                int j = itemIndex - 1;

                while (j >= l && itemValue <= A[j])
                {
                    A[j + 1] = A[j];
                    j -= 1;
                }

                A[j + 1] = itemValue;
            }
        }

        private static void ExampleQuicksort3WaysFast()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);
            foreach (var item in A)
                Console.Write(item + " ");

            QuickSort3WaysFast(A, 0, A.Length);

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

        private static bool IsSorted(int[] A)
        {
            for (int i = 1; i < A.Length; i++)
            {
                if (A[i] < A[i - 1])
                    return false;
            }
            return true;
        }

        private static void TestSorting()
        {
            for (int t = 0; t < 100; t++)
            {
                const int N = 100;
                Random rnd = new Random();
                int[] A = new int[N];
                for (int i = 0; i < A.Length; i++)
                    A[i] = rnd.Next(1, N);

                QuickSort3WaysFast(A, 0, A.Length);
                if (!IsSorted(A))
                {
                    foreach (var item in A)
                        Console.Write(item + " ");
                    Console.WriteLine();
                }
            }
        }
    }
}
