using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC_2021
{
    public class Day8
    {
        static List<int> GetPart1Input()
        {
            var lines = System.IO.File.ReadAllLines("inputs/day8.txt").ToList();
            var result = lines.Select(x => x.Split('|')[1])
                              .Select(x => x.Trim())
                              .SelectMany(x => x.Split(' ').ToList())
                              .Select(x => x.Length)
                              .ToList();
            return result;
        }

        public static string Part1()
        {
            var input = GetPart1Input();

            var digitSum = input.Count(x => x == 2 || x == 4 || x == 3 || x == 7);
            
            return $"D8P1: The number of 1, 4, 7, or 8 digits is {digitSum}";
        }


        static List<string> GetPart2Input()
        {
            var lines = System.IO.File.ReadAllLines("inputs/day8.txt").ToList();
            var result = lines.Select(x => x.Trim()).ToList();
            return result;

        }

        public static string Part2()
        {
            var input = GetPart2Input();

            var sum = 0;
            foreach (var line in input)
            {
                var dictionary = DeduceNumbers(line);
                var output = line.Split('|')[1].Trim();
                sum += TranslateOutput(output, dictionary);
            }
            return $"D8P2: The sum of the scrambled output digits is {sum}";
        }

        static int TranslateOutput(string input, Dictionary<string, char> dict)
        {
            var resultValue = new List<char>();
            var numberElements = input.Split(' ');
            foreach(var element in numberElements)
            {
                var sortedElement = element.ToList();
                sortedElement.Sort();
                var lookupKey = new string(sortedElement.ToArray());
                var number = dict[lookupKey];
                resultValue.Add(number);
            }
            var result = new string(resultValue.ToArray());
            return Convert.ToInt32(result);
        }

        static Dictionary<string, char> DeduceNumbers(string input)
        {
            var digitString = input.Replace("|", "").Trim();
            var digitList = digitString.Split(' ');

            var knownDigits = new Dictionary<char, string>();
            var sortedKnownDigits = new Dictionary<string, char>();
            var unknown5Digit = new List<string>();
            var unknown6Digit = new List<string>();
            
            foreach(var digit in digitList)
            {
                var partialDecode = GetStaticLengthDigit(digit);
                if (partialDecode == 'u')
                {
                    unknown5Digit.Add(digit);
                }
                else if (partialDecode == 'v')
                {
                    unknown6Digit.Add(digit);
                }
                else if (partialDecode == '-') continue;
                else
                {
                    if (!knownDigits.ContainsKey(partialDecode))
                    {
                        knownDigits.Add(partialDecode, digit);
                    }
                }
            }
            var two = unknown5Digit.FirstOrDefault(x => subtactLetters(x, knownDigits['4']).Length == 3);
            if (!string.IsNullOrWhiteSpace(two))
            {
                knownDigits.Add('2', two);
                unknown5Digit.RemoveAll(x => x == two);
            }
            var three = unknown5Digit.FirstOrDefault(x => subtactLetters(x, knownDigits['1']).Length == 3);
            if (!string.IsNullOrWhiteSpace(three))
            {
                knownDigits.Add('3', three);
                unknown5Digit.RemoveAll(x => x == three);
            }
            var five = unknown5Digit.FirstOrDefault(x => subtactLetters(x, knownDigits['4']).Length == 2);
            if (!string.IsNullOrWhiteSpace(five))
            {
                knownDigits.Add('5', five);
                unknown5Digit.RemoveAll(x => x == five);
            }
            var six = unknown6Digit.FirstOrDefault(x => subtactLetters(x, knownDigits['7']).Length == 4);
            if (!string.IsNullOrWhiteSpace(six))
            {
                knownDigits.Add('6', six);
                unknown6Digit.RemoveAll(x => x == six);
            }

            var zero = unknown6Digit.FirstOrDefault(x => subtactLetters(x, knownDigits['5']).Length == 2);
            if(!string.IsNullOrWhiteSpace(zero))
            {
                knownDigits.Add('0', zero);
                unknown6Digit.RemoveAll(x => x == zero);
            }
            var nine = unknown6Digit.FirstOrDefault(x => subtactLetters(x, knownDigits['5']).Length == 1);
            if (!string.IsNullOrWhiteSpace(nine))
            {
                knownDigits.Add('9', nine);
                unknown6Digit.RemoveAll(x => x == zero);
            }

            foreach(var element in knownDigits)
            {
                var value = element.Value.ToList();
                value.Sort();
                sortedKnownDigits.Add(new string(value.ToArray()), element.Key);
            }
            return sortedKnownDigits;
        }

        static string subtactLetters (string digit, string subtraction)
        {
            var result = new List<char>();
            foreach(var c in digit)
            {
                if (!subtraction.Contains(c)) result.Add(c);
            }
            return new string(result.ToArray());
        }

        
        static char GetStaticLengthDigit (string digit)
        {
            if (digit.Length == 2)
            {
                return '1';
            }
            else if (digit.Length == 4)
            {
                return '4';
            }
            else if (digit.Length == 3)
            {
                return '7';
            }
            else if (digit.Length == 7)
            {
                return '8';
            }
            else if (digit.Length == 5)
            {
                return 'u';
            }
            else if( digit.Length == 6)
            {
                return 'v';
            }
            return '-';
        }

    }

}