using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    struct KeyValuePair
    {
        int key;
        public int Key
        {
            get
            {
                return key;
            }
            set
            {
                if (key >= 0)
                    key = value;
                else
                    throw new Exception("Invalid key value");
            }
        }

        public int Value { get; set; }
    }

    class Program
	{
		static void Main(string[] args)
		{
            ExampleLSDRadixSortUseKeyValuePair();
		}

        private static int[] RadixSort(int[] A)
        {
            return RadixSortAux(A, 1);
        }

        private static int[] RadixSortAux(int[] A, int digitPlace)
        {
            bool Empty = true;
            int[] sorted = new int[A.Length];

            // Array holds all digits
            KeyValuePair[] digits = new KeyValuePair[A.Length];

            for (int i = 0; i < A.Length; i++)
            {
                digits[i] = new KeyValuePair() { Key = i, Value = (A[i] / digitPlace) % 10 };
                if (A[i] / digitPlace != 0)
                    Empty = false;
            }

            if (Empty)
                return A;

            KeyValuePair[] sortedDigits = CountingSort(digits);

            for (int i = 0; i < sorted.Length; i++)
                sorted[i] = A[sortedDigits[i].Key];
            return RadixSortAux(sorted, digitPlace * 10);
        }

        private static void ExampleLSDRadixSortUseKeyValuePair()
        {
            const int N = 100;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            int[] C = RadixSort(A);

            Console.WriteLine("\nSorted:");
            foreach (var item in C)
                Console.Write(item + " ");
            Console.WriteLine();
        }

        private static KeyValuePair[] CountingSort(KeyValuePair[] A)
        {
            KeyValuePair[] sorted = new KeyValuePair[A.Length];
            int[] counter = new int[GetMaxValue(A) + 1];

            // Increase the counter by number of occurrence of obj
            for (int i = 0; i < A.Length; i++)
                counter[A[i].Value]++;

            // Update counter array to contains actual positions in the output 
            for (int i = 1; i < counter.Length; i++)
                counter[i] += counter[i - 1];

            // Do it in array's reverse order to make counting sort stable
            for (int i = A.Length - 1; i >= 0; i--)
            {
                sorted[counter[A[i].Value] - 1] = new KeyValuePair { Key = i, Value = A[i].Value };
                counter[A[i].Value]--;
            }

            return sorted;
        }

        private static int GetMaxValue(KeyValuePair[] A)
        {
            int max = int.MinValue;
            for (int i = 0; i < A.Length; i++)
                if (A[i].Value > max)
                    max = A[i].Value;

            return max;
        }
    }
}
