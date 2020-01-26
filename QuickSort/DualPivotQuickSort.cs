using System;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleDualPivotQuickSort();
            TestDualPivotQuickSort();
        }

        private static Tuple<int, int> PartitionDualPivot(int[] A, int l, int r)
        {
            // This is Yaroslavskiy's dual-pivot partitioning scheme       
            // Reference: https://codeblab.com/wp-content/uploads/2009/09/DualPivotQuicksort.pdf

            if (A[l] > A[r - 1]) Swap(ref A[l], ref A[r - 1]);
            int pivot_1 = A[l], pivot_2 = A[r - 1];

            // Indices lt, kt, and gt such that Part I: A[lo+1...lt-1] < pivot_1, 
            // Part II: pivot _ 1 <= A[lt...kt-1] <= pivot_2 and 
            // Part III: A[gt+1...hi-2] > pivot_2 (!!! hi = r -1)
            int lt = l + 1, kt = l + 1, gt = r - 2;

            while (kt <= gt)
            {
                // Compare the next elem A[kt] with pivot_1 and pivot_2 and placed to the corresponding part I, II, III
                if (A[kt] < pivot_1)
                {
                    Swap(ref A[kt], ref A[lt]);
                    lt++;
                    kt++;
                }
                else if (A[kt] <= pivot_2)
                {
                    kt++;
                }
                else
                {
                    Swap(ref A[kt], ref A[gt]);
                    gt--;
                }               
            }

            // Swap pivot_1 with the last elem from Part I
            Swap(ref A[l], ref A[lt - 1]);

            // Swap pivot_2 with the first elem from Part III
            Swap(ref A[r - 1], ref A[gt + 1]);

            // return pointer end of Part I and end of Part II
            return new Tuple<int, int>(lt - 1, kt);
        }

        private const int INSERTION_SORT_CUTOFF = 27;
        private static void DualPivotQuickSort(int[] A, int l, int r)
        {
            // For small array, Use Insertion sort
            if (r - l <= INSERTION_SORT_CUTOFF)
            {
                InsertionSort(A, l, r);
                return;
            }

            // Use median-of-3 partitioning element
            int m = MedianOf3(A, l, new Random().Next(l, r), new Random().Next(l, r));
            Swap(ref A[l], ref A[m]);
            m = MedianOf3(A, r - 1, new Random().Next(l, r), new Random().Next(l, r));
            Swap(ref A[r - 1], ref A[m]);

            Tuple<int, int> pair = PartitionDualPivot(A, l, r);
            DualPivotQuickSort(A, l, pair.Item1);
            DualPivotQuickSort(A, pair.Item1, pair.Item2);
            DualPivotQuickSort(A, pair.Item2, r);
        }

        private static void ExampleDualPivotQuickSort()
        {
            const int N = 10;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);
            //int[] A = new int[] { 4, 3, 2, 1, 5, 4, 1, 8, 3, 7, 6 };
            foreach (var item in A)
                Console.Write(item + " ");            

            DualPivotQuickSort(A, 0, A.Length);

            Console.WriteLine("\nSorted:");
            foreach (var item in A)
                Console.Write(item + " ");
            Console.WriteLine();
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

        private static void TestDualPivotQuickSort()
        {
            for (int t = 0; t < 100; t++)
            {
                const int N = 1000;
                Random rnd = new Random();
                int[] A = new int[N];
                for (int i = 0; i < A.Length; i++)
                    A[i] = rnd.Next(1, N);

                DualPivotQuickSort(A, 0, A.Length);
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
