using System;

namespace CsharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleSearchKMP("abac", "abacadabrabracabracadabrabrabracad");
            ExampleSearchKMP("baca", "abacadabrabracabracadabrabrabracad");
            ExampleSearchKMP("abracadabra", "abacadabrabracabracadabrabrabracad");
            ExampleSearchKMP("rab", "abacadabrabracabracadabrabrabracad");
            ExampleSearchKMP("rabrabracad", "abacadabrabracabracadabrabrabracad");
            ExampleSearchKMP("bcara", "abacadabrabracabracadabrabrabracad");

            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Enter pattern:");
                string pattern = Console.ReadLine();

                Console.WriteLine("Enter text to find pattern in:");
                string txt = Console.ReadLine();

                int res = SearchKMP(pattern, txt);
                if (res == -1) Console.WriteLine("Pattern not found!");
                else Console.WriteLine("Pattern appears at: " + res);
            }
        }

        private static readonly int R = 256; // Radix (Here is the size of ASCII table)
        /// <summary>
        /// Search for the pattern in the input text using Knuth-Morris-Pratt algorithm
        /// THE IDEA: We try to remember the patterns we have seen before and only backtrack to that point without starting over again
        /// Reference: https://www.cs.princeton.edu/courses/archive/spring11/cos226/demo/53KnuthMorrisPratt.pdf
        /// </summary>
        /// <param name="pat">The pattern to search for</param>
        /// <param name="txt">The input text</param>
        /// <returns></returns>
        public static int SearchKMP(string pat, string txt)
        {
            if (pat.Length > txt.Length) return -1;

            // Build a Deterministic Finite Automata (DFA) from the pattern
            int[,] DFA = new int[R, pat.Length];

            DFA[pat[0], 0] = 1;
            for (int X = 0, j = 1; j < pat.Length; j++)
            {
                for (int c = 0; c < R; c++)
                    DFA[c, j] = DFA[c, X]; // Copy mismatch cases

                // For each state j, first (j + 1) characters of pattern have been matched
                DFA[pat[j], j] = j + 1; // Set match case: advance to next state if c == pat[j]
                X = DFA[pat[j], X]; // Update restart state
            }

            int a = 0, b = 0;
            while (a < txt.Length)
            {
                b = DFA[txt[a], b];
                a++;

                // b reached accepted state
                // This could be updated to return all indexes of found pattern in the input
                // Updated verion would be
                // if (b == pat.Length) store (a - pat.Length) ; reset b = 0 (inital state)
                if (b == pat.Length)
                    return a - pat.Length;
            }

            return -1;
        }

        private static void ExampleSearchKMP(string pat, string txt)
        {
            int res = SearchKMP(pat, txt);
            if (res == -1) Console.WriteLine(pat + " not found in " + txt);
            else Console.WriteLine(pat + " found at index " + res + " in " + txt);
        }

        /* NOTE: On How To Construct DFA tables step by step
         * Pattern: ABABAC -> pat.Length = 6 -> 6 State with state 0 as start state and state 6 as accepted state
         * 
         * I. Draw the DFA
         * s0 ---A---> s1 ---B---> s2 ---A---> s3 ---B---> s4 ---A---> s5 ---C---> s6
         * 
         * II. Draw the Transition table 
         *   | s0 | s1 | s2 | s3 | s4 | s5 |
         * A | 
         * B |
         * C |   
       D...Z | s0 | s0 | s0 | s0 | s0 | s0 |
         * 
         * III. Start filling out Tramsition table with match cases
         * 
         * s0 ---A---> s1, s1 ---B---> s2, etc
         * 
         *   | s0 | s1 | s2 | s3 | s4 | s5 |
         * A | s1 |    | s3 |    | s5 |
         * B |    | s2 |    | s4 |
         * C |                        | s6 |
       D...Z | s0 | s0 | s0 | s0 | s0 | s0 |
         * 
         * IV. Filling out mismatch cases
         * 
         * s0 ---B---> s0 ; s0 ---C---> s0
         * 
         * From this step onward, take the seq [s1...sj] + the letter being considered) and plug in the automata
         * 
         * s1 ---A---> s1 
         * In this case seq [s1...sj] is seq[1...1] which is nothing, nothing + A, plug in  s0 ---A---> s1 ===> s1 ---A---> s1
         * Result:
         *   | s0 | s1 | s2 | s3 | s4 | s5 |
         * A | s1 | s1 | s3 |    | s5 |
         * B | s0 | s2 |    | s4 |
         * C | s0 |                   | s6 |
       D...Z | s0 | s0 | s0 | s0 | s0 | s0 |
         * 
         * s3 ---A---> s1
         * In this case seq [s1...sj] is seq [s1...s3] which is B-A, B-A + A, plug in s0 ---B---> s0 ---A---> s1 ---A---> s1 ===> s3 ---A---> s1
         * Result:
         *   | s0 | s1 | s2 | s3 | s4 | s5 |
         * A | s1 | s1 | s3 | s1 | s5 |
         * B | s0 | s2 | s0 | s4 |
         * C | s0 | s0 | s0 |         | s6 |
         * 
         * -- Repeat these steps ---
         *   | s0 | s1 | s2 | s3 | s4 | s5 |
         * A | s1 | s1 | s3 | s1 | s5 | s1 |
         * B | s0 | s2 | s0 | s4 | s0 |
         * C | s0 | s0 | s0 | s0 | s0 | s6 |
       D...Z | s0 | s0 | s0 | s0 | s0 | s0 |
         *
         * s5 ---B---> s4
         * In this case seq [s1...sj] is [s1...s5] which is B-A-B-A, B-A-B-A + B, plug in so s0 ---B---> s0 ---A---> s1 ---B---> s2 ---A---> s3 ---B---> s4
         * Final Result:
         *   | s0 | s1 | s2 | s3 | s4 | s5 |
         * A | s1 | s1 | s3 | s1 | s5 | s1 |
         * B | s0 | s2 | s0 | s4 | s0 | s4 |
         * C | s0 | s0 | s0 | s0 | s0 | s6 |
       D...Z | s0 | s0 | s0 | s0 | s0 | s0 |
         */
    }
}
