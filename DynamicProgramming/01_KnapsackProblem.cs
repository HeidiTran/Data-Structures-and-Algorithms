using System;
using System.Collections.Generic;

namespace CSharpSample 
{
    class Program
    {
        static void Main(string[] args)
        {
            Example01Knapsack();
        }

        private static void Example01Knapsack()
        {
            int[] vals = new int[] { 16, 19, 23, 28 };
            int[] wt = new int[] { 2, 2, 4, 5 };
            
            int MaxW = 7;
            int numItems = vals.Length;

            Console.WriteLine("MaxValue: " + Knap(MaxW, wt, vals, numItems, true, true));
        }

        private static int Knap(int MaxWeight, int[] wt, int[] vals, int numItems, bool getSelectedItems = false, bool showKnapSackMatrix = false)
        {
            // The idea: What is the maximum value I can obtain with MaxWeight weights and i items
            int[,] KnapSack = new int[numItems, MaxWeight + 1];
            for (int i = 0; i < numItems; i++)
            {
                for (int j = 1; j <= MaxWeight; j++)
                {
                    if (i == 0)
                    {
                        if (j >= vals[0])
                            KnapSack[i, j] = vals[0];
                        else
                            KnapSack[i, j] = 0;
                    }
                    else if (j < wt[i])
                    {
                        KnapSack[i, j] = KnapSack[i - 1, j];
                    }
                    else
                    {
                        // Values of ith item + Maximum value obtained with (j - wt[i]) weights and (i - 1) items
                        int knapSackValueIfTake = vals[i] + KnapSack[i - 1, j - wt[i]];

                        // Maximum value obtained with j weights, and (i - 1) items
                        int knapSackValueIfNOTTake = KnapSack[i - 1, j];

                        // KnapSack[1, 5] = Math.Max(vals[1] + KnapSack[0, 5 - wt[1], KnapSack[0, 5]]
                        KnapSack[i, j] = Math.Max(knapSackValueIfTake, knapSackValueIfNOTTake);
                    }
                }
            }


            // Optionally show Knapsack Matrix
            if (showKnapSackMatrix)
            {
                Console.WriteLine("Knapsack Matrix: ");
                for (int i = 0; i < numItems; i++)
                {
                    for (int j = 0; j <= MaxWeight; j++)
                    {
                        Console.Write($"{KnapSack[i, j],5}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            // Reconstruct: What items were selected
            if (getSelectedItems)
            {
                List<int> selectedItems = new List<int>();

                int a = numItems - 1;
                int b = MaxWeight;
                while (a > 0)
                {
                    if (KnapSack[a, b] != KnapSack[a - 1, b])
                    {
                        selectedItems.Add(a);
                        b = b - wt[a];
                    }

                    a -= 1;
                }

                Console.Write("Items that are selected: ");
                foreach (var item in selectedItems)
                    Console.Write(item + " ");
                Console.WriteLine();
            }

            return KnapSack[numItems - 1, MaxWeight];
        }
    }
}
