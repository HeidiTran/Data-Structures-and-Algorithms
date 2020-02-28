using System;
using System.Numerics;

namespace CsharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleSearchRabinKarp("abac", "abacadabrabracabracadabrabrabracad");
            ExampleSearchRabinKarp("baca", "abacadabrabracabracadabrabrabracad");
            ExampleSearchRabinKarp("abracadabra", "abacadabrabracabracadabrabrabracad");
            ExampleSearchRabinKarp("rab", "abacadabrabracabracadabrabrabracad");
            ExampleSearchRabinKarp("rabrabracad", "abacadabrabracabracadabrabrabracad");
            ExampleSearchRabinKarp("bcara", "abacadabrabracabracadabrabrabracad");

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

        private static readonly int R = 256; // Radix (Here is the size of ASCII table)
        private static long Q = LongRandomPrime(); // The working module

        /// <summary>
        /// Produce a 31-bit prime number (small enough to avoid long overflow when casting)
        /// Maximum value of 31 bit number is 2^31 - 1
        /// </summary>
        private static long LongRandomPrime()
        {
            BigInteger prime = 1179431497;
            //BigInteger prime = 1998018409
            return (long)prime;
        }

        /// <summary>
        /// Search for the pattern in the input text using Rabin-Karp algorithm
        /// It calculates the hash value of the pattern as well as pattern.length-character sequences in the input
        /// Depending on the hashing function, worst case takes O(nm) time with n, m as lengths of the pattern and the input, and best case is O(n+m)
        /// </summary>
        /// <param name="pat">The pattern to search for</param>
        /// <param name="txt">The input text</param>
        /// <returns>Index of the first match character or -1 if not found</returns>
        public static int Search(string pat, string txt)
        {
            if (pat.Length > txt.Length)
                return -1;

            // Calculate H for use in rehashing
            // H = R^(pattern.Length - 1) % Q
            long H = 1;
            for (int i = 1; i <= pat.Length - 1; i++)
                H = (R * H) % Q;

            long patternHash = Hash(pat, pat.Length);
            long txtHash = Hash(txt, pat.Length);

            // Check if match at shift 0
            if (patternHash == txtHash && IsMatch(pat, txt, 0)) return 0;

            //// Check if match at shift 1 -> txt.Length - pat.Length
            for (int shift = 1; shift <= txt.Length - pat.Length; shift++)
            {
                // Eg: txt = "23456", pat.Length = 3, Q = 1
                // txtHash for ("234") = 234 -> txtHash for ("345") = [ 234 - 100 * 2 ] * 10 + 5
                // Here () % Q + Q just to make sure txtHash will always be positive
                txtHash = (txtHash - (H * txt[shift - 1]) % Q + Q) % Q; // Remove leading digit
                txtHash = (txtHash * R + txt[shift - 1 + pat.Length]) % Q; // Add trailing digit

                if (patternHash == txtHash && IsMatch(pat, txt, shift)) return shift;
            }

            return -1;
        }

        private static long Hash(string str, int len)
        {
            long res = 0;
            for (int i = 0; i < len; i++)
                res = (res * R + str[i]) % Q;

            return res;
        }

        private static bool IsMatch(string pat, string txt, int pos)
        {
            for (int i = 0; i < pat.Length; i++)
            {
                if (pat[i] != txt[i + pos])
                    return false;
            }

            return true;
        }

        private static void ExampleSearchRabinKarp(string pat, string txt)
        {
            int res = Search(pat, txt);
            if (res == -1) Console.WriteLine(pat + " not found in " + txt);
            else Console.WriteLine(pat + " found at index " + res + " in " + txt);
        }
    }
}
