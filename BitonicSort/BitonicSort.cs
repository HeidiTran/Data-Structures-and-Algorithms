using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleBitonicSort();
            //TestSorting();
        }

        public enum Direction
        {
            Ascending,
            Descending
        }

        public static void BitonicSort(int[] A, int l, int r, Direction direction = Direction.Ascending)
        {
            if (!IsPowerOfTwo(r - l))
                throw new InvalidOperationException("Bitonic Sort only works for 2^k (positive k) numbers of elements.");

            if (r - l > 1)
            {
                int half = (r - l) / 2;

                // Sort left side in ascending order
                BitonicSort(A, l, l + half, Direction.Ascending);

                // Sort right side in descending order
                BitonicSort(A, l + half, r, Direction.Descending);

                // Merge entire sequence in ascending order
                BitonicMerge(A, l, r, direction);
            }
        }

        public static void BitonicMerge(int[] A, int l, int r, Direction direction)
        {
            if (r - l > 1)
            {
                int half = (r - l) / 2;
                for (int i = l; i < l + half; i++)
                {
                    CompareAndSwap(A, i, i + half, direction);
                }

                BitonicMerge(A, l, l + half, direction);
                BitonicMerge(A, l + half, r, direction);
            }
        }

        public static void CompareAndSwap(int[] A, int i, int j, Direction direction)
        {
            Direction k = A[i] > A[j] ? Direction.Descending : Direction.Ascending;

            if (k != direction)
            {
                Swap(ref A[i], ref A[j]);
            }
        }

        private static void ExampleBitonicSort()
        {
            const int N = 128;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);
            //int[] A = { 3, 7, 4, 8 };
            foreach (var item in A)
                Console.Write(item + " ");

            BitonicSort(A, 0, A.Length);

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

        private static bool IsPowerOfTwo(int x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
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
                const int N = 128;
                Random rnd = new Random();
                int[] A = new int[N];
                for (int i = 0; i < A.Length; i++)
                    A[i] = rnd.Next(1, N);

                BitonicSort(A, 0, A.Length);
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
