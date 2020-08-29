using System;
using System.Collections.Generic;
using System.Linq;

namespace TestAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test 1: Call IEnumerable with ToArray().");
            foreach (var val in GetValues().ToArray())
            {
                Console.WriteLine($"Returned value {val}");
            }

            Console.WriteLine();
            Console.WriteLine("Test 2: Call IEnumerable normally.");
            foreach (var val in GetValues())
            {
                Console.WriteLine($"Returned value {val}");
            }
        }

        static IEnumerable<int> GetValues()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"About to yield return {i}");
                yield return i;
            }
        }
    }
}
