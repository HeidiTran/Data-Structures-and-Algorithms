using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{   
    class Program
    {
        static void Main(string[] args)
        {
            ExampleLinearSearch();
        }

        private static void ExampleLinearSearch()
        {
            const int N = 25;
            Random rnd = new Random();
            int[] A = new int[N];
            for (int i = 0; i < A.Length; i++)
                A[i] = rnd.Next(1, N);

            foreach (var item in A)
                Console.Write(item + " ");

            while (true)
            {
                Console.WriteLine("\nPlease enter a number to find: ");
                int item = Convert.ToInt32(Console.ReadLine());
                int ind = LinearSearch(A, 0, A.Length - 1, item);

                if (ind == -1)
                    Console.WriteLine("Item " + item + " does not exists!");
                else
                    Console.WriteLine("Item " + item + " appears at position " + ind);
            }
        }

        private static int LinearSearch(int[] A, int lo, int hi, int val)
        {
            for (int i = lo; i < hi; i++)
            {
                if (A[i] == val)
                    return i;
            }

            return -1;
        }
    }
}
