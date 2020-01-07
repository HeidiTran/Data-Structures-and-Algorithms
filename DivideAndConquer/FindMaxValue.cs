using System;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 4, 3, 2, 1 };
            int res = Max(numbers, 0, numbers.Length - 1);
            Console.WriteLine("Maximum value in this array is: " + res);
        }

        private static int Max(int[] A, int l, int r)
        {
            // Note: Runtime complexity is O(n) as T(n) = 2T(n/2) + 1
            if (l == r)
                return A[l];

            int mid = (r + l) / 2;
            int u = Max(A, l, mid);
            int v = Max(A, mid + 1, r);

            if (u > v)
                return u;
            else
                return v;
        }
    }
}
