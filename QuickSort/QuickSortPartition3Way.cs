using System;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleQuickSort3Ways();
        }

        private static void QuickSort3Ways(int[] A, int l, int r)
        {
            // Entropy optimal Sorting: Partion array into 3 parts, one smaller than the pivot, one equal to the pivot, and
            // one larger than the pivot
            if (r - 1 <= l)
                return;

            Tuple<int, int> pair = Partition3Parts(A, l, r);
            QuickSort3Ways(A, l, pair.Item1);
            QuickSort3Ways(A, pair.Item2, r);
        }

        private static Tuple<int, int> Partition3Parts(int[] A, int l, int r)
        {
            // Dijkstra's partition scheme is used here
            // "Dijkstra's solution is based on a single left-to-right pass through the array that maintains a pointer
            // lt such that a[lo..lt-1] is less than v, a pointer gt such that a[gt+1..hi] is greater than v, and a 
            // pointer i such that a[lt..i-1] are equal to v, and a[i..gt] are not yet examined"
            int lt = l, gt = r - 1, i = l + 1;
            int pivot = A[l];
            while (true)
            {
                if (A[i] < pivot)
                {
                    Swap(ref A[lt], ref A[i]);
                    lt++;
                    i++;
                }
                else if (A[i] > pivot)
                {
                    Swap(ref A[i], ref A[gt]);
                    gt--;
                }
                else
                    i++;

                if (i > gt)
                    break;
            }

            return new Tuple<int, int>(lt, gt + 1);
        }

        private static void ExampleQuickSort3Ways()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            QuickSort3Ways(A, 0, A.Length);

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
