using System;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleShellSort();
            //TestSorting();
        }

        private static void ShellSort(int[] A, int l, int r)
        {
            int n = r - l;
            int gap = 1;

            // 3x + 1 increment sequence is used here
            while (gap < n / 3) gap = 3 * gap + 1;

            while (gap >= 1)
            {
                // h-sort the array
                for (int i = gap; i < n; i++)
                {
                    for (int j = i; j >= gap && A[j] < A[j - gap]; j -= gap)
                    {
                        Swap(ref A[j], ref A[j - gap]);
                    }
                }

                gap /= 3;
            }
        }

        private static void ExampleShellSort()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            ShellSort(A, 0, A.Length);

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

                ShellSort(A, 0, A.Length);
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
