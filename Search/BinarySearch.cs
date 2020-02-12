using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleBinarySearch();
        }

        /// <summary>
        /// Given a sorted list, return the first index found of val if exists, and -1 otherwise
        /// </summary>
        private static int BinarySearch(int[] A, int lo, int hi, int val)
        {
            if (hi < lo) return -1;

            int mid = lo + (hi - lo) / 2;

            if (A[mid] == val && (mid == 0 || A[mid - 1] < val))
                return mid;
            else if (A[mid] < val)
                return BinarySearch(A, mid + 1, hi, val);
            else
                return BinarySearch(A, lo, mid - 1, val);

        }

        private static int BinarySearchLast(int[] A, int lo, int hi, int val)
        {
            if (hi < lo) return -1;

            int mid = lo + (hi - lo) / 2;

            if (A[mid] == val && (mid == A.Length - 1 || A[mid + 1] > val))
                return mid;
            else if (A[mid] > val)
                return BinarySearchLast(A, lo, mid - 1, val);
            else
                return BinarySearchLast(A, mid + 1, hi, val);

        }

        private static List<int> BinarySearchArr(int[] A, int lo, int hi, int val)
        {
            // Time complexity is O(log(n)) because BinarySearch takes O(log(n)) and BinarySearchLast takes O(log(n))
            int firstIndex = BinarySearch(A, lo, hi, val);

            if (firstIndex == -1) return new List<int>();

            int lastIndex = BinarySearchLast(A, lo, hi, val);

            List<int> inds = new List<int>();
            for (int i = firstIndex; i <= lastIndex; i++)
                inds.Add(i);
            return inds;
        }

        private static void ExampleBinarySearch()
        {
            const int N = 25;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            Array.Sort(A);
            foreach (var item in A)
                Console.Write(item + " ");

            while (true)
            {
                Console.WriteLine("\nPlease enter a number to find: ");
                int item = Convert.ToInt32(Console.ReadLine());
                int ind = BinarySearch(A, 0, A.Length - 1, item);

                if (ind == -1)
                    Console.WriteLine("Item " + item + " does not exists!");
                else
                    Console.WriteLine("Item " + item + " appears at position " + ind);

                List<int> positions = BinarySearchArr(A, 0, A.Length - 1, item);
                if (positions.Count != 0)
                {
                    Console.WriteLine("Item " + item + " appears at these positions");
                    foreach (var pos in positions)
                        Console.Write(pos + " ");
                }
            }
        }
    }
}
