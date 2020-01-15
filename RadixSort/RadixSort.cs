using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
	class Program
	{
		static void Main(string[] args)
		{
			ExampleRadixSort();
		}
		
		// Least Significant Digit Radix Sort
		private static void LSDRadixSort(int[] A, int l, int r)
		{
			int largestElem = GetMaxElem(A, l, r);
			int maxDigit = Convert.ToInt32(Math.Floor(Math.Log10(largestElem) + 1));
			int[,] bucket = new int[10, r];

			// Put arr into bucket based on dg-th digit
			for (int dg = 0; dg < maxDigit; dg++)
			{
				ResetBucket(bucket);

				for (int i = l; i < r; i++)
				{
					int digit = A[i];
					for (int m = 0; m < dg; m++)
						digit /= 10;
					digit %= 10;

					// bucket[digit, ...] contains all number than has dg-th digit = digit
					for (int j = 0; j < r; j++)
					{
						if (bucket[digit, j] == int.MinValue)
						{
							bucket[digit, j] = A[i];
							break;
						}
					}
				}

				AssignBucketToArray(A, l, r, bucket);
			}
		}

		private static void ResetBucket(int[,] A)
		{
			for (int i = 0; i < A.GetLength(0); i++)
				for (int j = 0; j < A.GetLength(1); j++)
					A[i, j] = int.MinValue;
		}

		private static void AssignBucketToArray(int[] A, int l, int r, int[,] bucket)
		{
			int k = l;
			for (int i = 0; i < bucket.GetLength(0); i++)
			{
				for (int j = 0; j < bucket.GetLength(1); j++)
				{
					if (bucket[i, j] != int.MinValue)
					{
						A[k] = bucket[i, j];
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

		private static void ExampleRadixSort()
		{
			const int N = 100;
			Random rnd = new Random();
			int[] A = new int[N];
			for (int i = 0; i < A.Length; i++)
				A[i] = rnd.Next(1, N);

			foreach (var item in A)
				Console.Write(item + " ");

			LSDRadixSort(A, 0, A.Length);

			Console.WriteLine("\nSorted:");
			foreach (var item in A)
				Console.Write(item + " ");
			Console.WriteLine();
		}
	}
}
