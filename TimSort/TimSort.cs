using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample 
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleTimSort();
        }

        private static int[] TimSort(int[] A, int l, int r)
        {
            // The idea: Divide array into blocks, Insertion sort each block and Merge all the blocks
            // This overcomes Insertion Sort weakness (inefficient on long array) and Merge Sort weakness
            // (2 fixed size array requires a lot of comparisons)
            // Time complexity: Average Case = O(n log n)
            // In practice, blockSize ranges from 32-65
            const int blockSize = 32;

            for (int i = 0; i < A.Length; i += blockSize)
                InsertionSort(A, i, Math.Min(i + blockSize, r));

            for (int i = 0; i < A.Length; i += blockSize)
                Merge(A, 0, Math.Min(i + blockSize, r), Math.Min(i + blockSize * 2, r));

            return A;
        }

        private static void Merge(int[] A, int l, int mid, int r)
        {
            int[] firstHalf = new int[mid - l];
            int[] secondHalf = new int[r - mid];

            for (int t = 0; t < mid; t++)
                firstHalf[t] = A[l + t];

            for (int t = mid; t < r; t++)
                secondHalf[t - mid] = A[t];

            int i = 0, j = 0, k = l;

            while (i < firstHalf.Length && j < secondHalf.Length)
            {
                if (firstHalf[i] <= secondHalf[j])
                {
                    A[k] = firstHalf[i];
                    i++;
                }
                else
                {
                    A[k] = secondHalf[j];
                    j++;
                }
                k++;
            }

            if (i == firstHalf.Length)
            {
                while (j < secondHalf.Length)
                {
                    A[k] = secondHalf[j];
                    j++; k++;
                }
            }
            else
            {
                while (i < firstHalf.Length)
                {
                    A[k] = firstHalf[i];
                    i++; k++;
                }
            }
        }

        private static int[] InsertionSort(int[] A, int l, int r)
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

            return A;
        }

        private static void ExampleTimSort()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            int[] C = TimSort(A, 0, A.Length);

            Console.WriteLine("\nSorted:");
            foreach (var item in C)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
