using System;
using System.Collections.Generic;

namespace CSharpSample 
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleMergeSort();
        }        

        // MergeSort also belongs to the Divide and Conquer technique
        private static int[] MergeSort(int[] A, int l, int r)
        {
            if (r - l <= 1)
                return new int[] { A[l] };

            int mid = (l + r) / 2;
            int[] firstHalfSorted = MergeSort(A, l, mid);
            int[] secondHalfSorted = MergeSort(A, mid, r);

            return Merge(firstHalfSorted, secondHalfSorted);
        }

        private static int[] Merge(int[] A, int[] B)
        {
            int[] C = new int[A.Length + B.Length];
            int i = 0, j = 0, k = 0;
            while (i < A.Length && j < B.Length)
            {
                if (A[i] <= B[j])
                {
                    C[k] = A[i];
                    i++;
                }
                else
                {
                    C[k] = B[j];
                    j++;
                }
                k++;
            }

            if (i == A.Length)
            {
                while (j < B.Length)
                {
                    C[k] = B[j];
                    j++;
                    k++;
                }

            }
            else
            {
                while (i < A.Length)
                {
                    C[k] = A[i];
                    i++;
                    k++;
                }
            }

            return C;
        }

        private static void ExampleMergeSort()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            int[] C = MergeSort(A, 0, A.Length);

            Console.WriteLine("\nSorted:");
            foreach (var item in C)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
