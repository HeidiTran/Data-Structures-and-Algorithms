using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpSample 
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleEggDropProblem();
        }

        private static void ExampleEggDropProblem()
        {
            int floors = 100;
            int numEggs = 2;

            Console.WriteLine("Minimun of drops needed for " + numEggs + " eggs and " + floors + " floors: " + EggDrop(floors, numEggs, true));
        }

        private static int EggDrop(int floors, int eggs, bool getSelectedFloors = false, bool showEggDropMatrix = false)
        {
            int[,] eggMatrix = new int[eggs + 1, floors + 1];
            List<int> selectedFloor = new List<int>() { floors };

            // If we have i eggs and 1 floor
            for (int i = 1; i <= eggs; i++)
                eggMatrix[i, 1] = 1;

            // If we have one egg and j floors
            for (int j = 1; j <= floors; j++)
                eggMatrix[1, j] = j;

            for (int i = 2; i <= eggs; i++)
            {
                for (int j = 2; j <= floors; j++)
                {
                    eggMatrix[i, j] = int.MaxValue;
                    // Try to drop the first egg from 1-j floor
                    for (int x = 1; x <= j; x++)
                    {
                        // If the egg breaks
                        int a = eggMatrix[i - 1, x - 1];

                        // If the egg doesn't break
                        int b = eggMatrix[i, j - x];

                        // Number of attempts in the worst case -> use Max + 1 (we've already used the first egg)
                        int res = Math.Max(a, b) + 1;

                        // The min attempts needed: this happen when the first egg is dropped at the "right" floor
                        if (res < eggMatrix[i, j])
                        {
                            if (getSelectedFloors && eggMatrix[i, j] != int.MaxValue)
                                selectedFloor.Add(x + (floors - j));
                            eggMatrix[i, j] = res;
                        }
                    }
                }
            }

            if (getSelectedFloors)
            {
                Console.WriteLine("Selected floors: ");
                selectedFloor.Reverse();
                foreach (var item in selectedFloor.Distinct())
                    Console.Write(item + " ");
                Console.WriteLine();
            }

            if (showEggDropMatrix)
            {
                Console.WriteLine("EggDrop Matrix: ");
                for (int i = 0; i <= eggs; i++)
                {
                    for (int j = 0; j <= floors; j++)
                        Console.Write($"{eggMatrix[i, j],5}");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            return eggMatrix[eggs, floors];
        }
    }
}
