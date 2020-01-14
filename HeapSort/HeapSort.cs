using System;
using System.Collections.Generic;

namespace CSharpSample 
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleHeapSort();
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        private static void TopDownHeapify(int[] A, int size, int k)
        {
            // No node in a heap-ordered tree has a key larger than the key at the root
            int maxNodeIndex = k;
            int l = 2 * k + 1; // Left child of node ith is (2*i + 1)th
            int r = 2 * k + 2; // Right child of node ith is (2*i + 2)th

            if (l < size && A[l] > A[maxNodeIndex])
                maxNodeIndex = l;

            if (r < size && A[r] > A[maxNodeIndex])
                maxNodeIndex = r;

            // If left node or right node is largest than root node
            if (maxNodeIndex != k)
            {
                Swap(ref A[k], ref A[maxNodeIndex]);
                TopDownHeapify(A, size, maxNodeIndex);
            }
        }

        private static void HeapSort(int[] A)
        {
            // Time complexity of average case and worst case is O (nlogn)
            // Build a Max-Heap
            for (int i = (A.Length / 2) - 1; i >= 0; i--)
                TopDownHeapify(A, A.Length, i);

            // Reapeated delete root node -> Swap it to the end
            for (int heapSize = A.Length - 1; heapSize >= 0; heapSize--)
            {
                Swap(ref A[0], ref A[heapSize]);
                TopDownHeapify(A, heapSize, 0);
            }
        }

        private static void ExampleHeapSort()
        {
            const int N = 10;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            HeapSort(A);

            Console.WriteLine("\nSorted:");
            foreach (var item in A)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
