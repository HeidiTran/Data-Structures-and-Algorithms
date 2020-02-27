using System;

namespace CsharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleSearchBruteForce("abracadabra", "abacadabrabracabracadabrabrabracad");
            ExampleSearchBruteForce("rab", "abacadabrabracabracadabrabrabracad");
            ExampleSearchBruteForce("rabrabracad", "abacadabrabracabracadabrabrabracad");
            ExampleSearchBruteForce("bcara", "abacadabrabracabracadabrabrabracad");

            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Enter pattern:");
                string pattern = Console.ReadLine();

                Console.WriteLine("Enter text to find pattern in:");
                string txt = Console.ReadLine();

                int res = Search(pattern, txt);
                if (res == -1) Console.WriteLine("Pattern not found!");
                else Console.WriteLine("Pattern appears at: " + res);
            }
        }

        /// <summary>
        /// Search for the pattern in the input text using brute force
        /// </summary>
        /// <param name="pattern">The pattern to search for</param>
        /// <param name="txt">The input text</param>
        /// <returns>Index of the first match character or -1 if not found</returns>
        public static int Search(string pattern, string txt)
        {
            if (pattern.Length > txt.Length)
                return -1;

            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] == pattern[0])
                {
                    int j = 0;
                    while (j < pattern.Length)
                    {
                        if (pattern[j] != txt[i + j])
                            break;

                        j++;
                    }

                    if (j == pattern.Length)
                        return i;
                }
            }

            return -1;
        }

        private static void ExampleSearchBruteForce(string pat, string txt)
        {
            int res = Search(pat, txt);
            if (res == -1) Console.WriteLine(pat + " not found in " + txt);
            else Console.WriteLine(pat + " found at index " + res + " in " + txt);
        }
    }
}




