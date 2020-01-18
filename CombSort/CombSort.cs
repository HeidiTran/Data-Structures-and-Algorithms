using System;

namespace CSharpSample
{
    class Program
	{
		static void Main(string[] args)
		{
            ExampleCombSort();
        }

        private static void CombSort(int[] A, int l, int r)
        {
            // The idea: Advanced Bubble Sort. Instead of swapping adjacent element (gap = 1) we have changing gap size
            int gap = r;

            while (true)
            {
                int swap = 0;
                gap = Convert.ToInt32(Math.Floor(gap / 1.3)) > 1 ? Convert.ToInt32(Math.Floor(gap / 1.3)) : 1;
                for (int i = l; i + gap < r; i++)
                {
                    if (A[i] > A[i + gap])
                    {
                        Swap(ref A[i], ref A[i + gap]);
                        swap++;
                    }
                }

                if (gap == 1 && swap == 0)
                    break;
            }
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        private static void ExampleCombSort()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            CombSort(A, 0, A.Length);

            Console.WriteLine("\nSorted:");
            foreach (var item in A)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
