using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2021
{
    public class Day1
    {
        static List<int> GetInput()
        {
            string[] lines = System.IO.File.ReadAllLines("inputs/day1.txt");
            var result = lines.Select(x => int.Parse(x)).ToList();
            return result;
        }

        public static string Part1()
        {
            var input = GetInput();
            var largerCount = 0;
            for (var i = 0; i < input.Count; i++)
            {
                if (i > 0 && input[i] > input[i-1])
                {
                    largerCount++;
                }
            }
            var result = $"D1P1: There are {largerCount} measurements greater than the previous measurment.";
            return result;
        }

        public static string Part2()
        {
            var input = GetInput();
            var largerCount = 0;
            var previousSum = 0;
            for (var i = 0; i < input.Count; i++)
            {
                if (i > 1)
                {
                    var sum = input[i] + input[i - 1] + input[i - 2];
                    if((previousSum != 0) && (sum > previousSum)) largerCount++;
                    previousSum = sum;
                }
            }
            var result = $"D1P2: There are {largerCount} sliding measurements greater than the previous measurment.";
            return result;
        }
    }
}
