using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private static Tuple<int, int, int> PartitionThreePivot(int[] A, int l, int r)
        {
            // !!!REQUIRE: A[l] < A[l + 1] < A[r - 1]

            int pivot_1 = A[l], pivot_2 = A[l + 1], pivot_3 = A[r - 1];

            // Indices a, b, c, and d such that
            // Part I: A[lo+2...a-1] < pivot_1
            // Part II: pivot_1 <= A[a...b-1] <= pivot_2
            // Part III: pivot_2 < A[c+1...d] <= pivot_3
            // Part IV: A[d+1...r-2] > pivot_3
            int a = l + 2, b = l + 2, c = r - 2, d = r - 2;

            while (b <= c)
            {
                if (A[b] <= pivot_2)
                {
                    if (A[b] < pivot_1)
                    {
                        Swap(ref A[b], ref A[a]);
                        a++;
                        b++;
                    }
                    else
                    {
                        b++;
                    }
                }
                else
                {
                    if (A[b] <= pivot_3)
                    {
                        Swap(ref A[b], ref A[c]);
                        c--;
                    }
                    else
                    {
                        Swap(ref A[b], ref A[d]);
                        d--;
                        Swap(ref A[b], ref A[c]);
                        c--;
                    }
                }
            }

            a--; b--; c++; d++;

            Swap(ref A[l + 1], ref A[a]);
            Swap(ref A[a], ref A[b]);
            a--;
            Swap(ref A[l], ref A[a]);
            Swap(ref A[r - 1], ref A[d]);

            return new Tuple<int, int, int>(a, c, d);
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

    }
}
