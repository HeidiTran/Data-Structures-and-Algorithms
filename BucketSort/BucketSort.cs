using System;
using System.Collections.Generic;

namespace CSharpSample
{
    class Program
	{
		static void Main(string[] args)
		{
            ExampleBucketSort();
        }

        private static void BucketSort(int[] A, int l, int r, int bucketsCnt)
        {
            // The idea: Put numbers into bucket based on range (The numbers should be uniformly distributed). Sort each bucket and merge all them back
            // Advantage: Sort large amount of data because each time we only sort a small amount

            // Find bucket interval
            int minElem = GetMinElem(A, l, r);
            int maxElem = GetMaxElem(A, l, r);

            int interval = (maxElem - minElem) / bucketsCnt != 0 ? (maxElem - minElem) / bucketsCnt : 1;

            List<int>[] buckets = new List<int>[bucketsCnt + 1];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = new List<int>();

            // Put number into bucket
            for (int i = l; i < r; i++)
                buckets[Convert.ToInt32(Math.Floor((double)(A[i] - minElem) / interval))].Add(A[i]);

            // Sort each bucket. Usually use Insertion sort because of small range between values
            foreach (var bucket in buckets)
                bucket.Sort();

            int k = l;
            foreach (var bucket in buckets)
            {
                if (bucket.Count != 0)
                {
                    foreach (var item in bucket)
                    {
                        A[k] = item;
                        k++;
                    }
                }
            }
        }

        private static int GetMaxElem(int[] A, int l, int r)
        {
            int max = int.MinValue;
            for (int i = l; i < r; i++)
                if (A[i] > max)
                    max = A[i];

            return max;
        }

        private static int GetMinElem(int[] A, int l, int r)
        {
            int min = int.MaxValue;
            for (int i = l; i < r; i++)
                if (A[i] < min)
                    min = A[i];

            return min;
        }

        private static void ExampleBucketSort()
        {
            const int N = 1000;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            BucketSort(A, 0, A.Length, 10);

            Console.WriteLine("\nSorted:");
            foreach (var item in A)
                Console.Write(item + " ");
            Console.WriteLine();
        }
    }
}
