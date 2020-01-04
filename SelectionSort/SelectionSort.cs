using System;

namespace CSharpSample
{
    class Program
    {
        static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        static int SmallestItemIndex(int[] A, int startPos)
        {
            int minValIndex = startPos;
            for (int i = startPos; i < A.Length; i++)
            {
                if (A[i] < A[minValIndex])
                {
                    minValIndex = i;
                }
            }

            return minValIndex;
        }

        private static void SelectionSort(int[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                int smallestItemIndex = SmallestItemIndex(A, i);
                Swap(ref A[i], ref A[smallestItemIndex]);
            }
        }

        private static void ExampleSelectionSort()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            SelectionSort(A);

            Console.WriteLine("\nSorted:");
            foreach (var item in A)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            ExampleSelectionSort();
        }
    }
}
