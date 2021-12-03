using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2021
{
    public class Day3
    {
        static List<string> GetInput()
        {
            var lines = System.IO.File.ReadAllLines("inputs/day3.txt").ToList();
            return lines;
        }

        public static string Part1()
        {
            var lines = GetInput();
            
            var countValues = new int[lines[0].Length];
            foreach (var line in lines)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1') countValues[i]++;
                }
            }

            var gammaRateString = new string(countValues.Select(x => x > lines.Count / 2 ?  '1' : '0').ToArray());
            var epislonRateString = new string(countValues.Select(x => x > lines.Count / 2 ? '0' : '1').ToArray());

            var gammaRate = Convert.ToInt32(gammaRateString, 2);
            var epislonRate = Convert.ToInt32(epislonRateString, 2);

            var result = gammaRate * epislonRate;

            return $"D3P1: The multipled gamma and epsilon rate values is {result}";
        }

        public static string Part2()
        {
            var lines = GetInput();

            var mostCommon = RecurseSolve(lines, 0, true).FirstOrDefault();
            var leastCommon = RecurseSolve(lines, 0, false).FirstOrDefault();

            var mostCommonValue = Convert.ToInt32(mostCommon,2);
            var leastCommonValue = Convert.ToInt32(leastCommon,2);

            var result = mostCommonValue * leastCommonValue;
            
            return $"D3P2: The life support rating is {result}";
        }

        public static List<string> RecurseSolve(List<string> input, int counterLevel, bool checkMostCommon)
        {
            char mostCommon;
            var mostCommonList = new List<string>();
            var leastCommonList = new List<string>();

            var oneCount = input.Count(x => x[counterLevel] == '1');
            var zeroCount = input.Count - oneCount;
            
            if (oneCount >= zeroCount) mostCommon = '1';
            else mostCommon = '0';

            foreach (var line in input)
            {
                if (line[counterLevel] == mostCommon) mostCommonList.Add(line);
                else leastCommonList.Add(line);
            }
            
            if (checkMostCommon) input = mostCommonList;
            else input = leastCommonList;

            if (input.Count == 1) return input;
            return RecurseSolve(input, counterLevel + 1, checkMostCommon);
        }
    }
}
